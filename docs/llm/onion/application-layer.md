# Application Layer Project Setup

This document shows the guidelines to create an APplication Layer in the Onion Architecture.
Main reference guidelines:

- ✅ Reference to Domain project only
- ✅ Application-specific packages (MediatR, FluentValidation, AutoMapper)
- ❌ No Infrastructure project reference
- ❌ No database-specific packages

## Application Layer Structure

the Application layer organizes the code by area and then by components or Use Cases.
considering an areas, let's see how the code is arranged on Board Example:

```txt
Application/
├── Boards/
│   ├── Models/
│   │   ├── MatchDTO.cs
│   │   ├── MatchResultDTO.cs
│   │   ├── TeamDTO.cs
```

```txt
Application/
├── Commands/
│   ├── CreateOrder/
│   │   ├── CreateOrderCommand.cs
│   │   ├── CreateOrderCommandHandler.cs
│   │   └── CreateOrderCommandValidator.cs
│   └── CancelOrder/
│       ├── CancelOrderCommand.cs
│       └── CancelOrderCommandHandler.cs
├── Queries/
│   ├── GetOrder/
│   │   ├── GetOrderQuery.cs
│   │   ├── GetOrderQueryHandler.cs
│   │   └── OrderDto.cs
│   └── GetCustomerOrders/
├── Services/
│   ├── IEmailService.cs
│   ├── IPaymentService.cs
│   └── OrderApplicationService.cs
├── DTOs/
│   ├── OrderDto.cs
│   ├── CustomerDto.cs
│   └── CreateOrderRequest.cs
├── Behaviors/
│   ├── ValidationBehavior.cs
│   ├── LoggingBehavior.cs
│   └── TransactionBehavior.cs
├── Mappings/
│   └── OrderMappingProfile.cs
├── Validators/
│   ├── CreateOrderValidator.cs
│   └── UpdateOrderValidator.cs
└── Exceptions/
    ├── ValidationException.cs
    └── ApplicationException.cs
```

## Application Layer Implementation Examples

### Command Handler with CQRS

```csharp
namespace YourCompany.YourDomain.Application.Commands.CreateOrder;

/// <summary>
/// Command to create a new order with specified items and customer information.
/// </summary>
/// <param name="CustomerId">The unique identifier of the customer placing the order.</param>
/// <param name="Items">List of items to include in the order.</param>
/// <param name="ShippingAddress">The address where the order should be shipped.</param>
public record CreateOrderCommand(
    CustomerId CustomerId,
    IList<OrderItemRequest> Items,
    Address ShippingAddress) : IRequest<OrderDto>;

/// <summary>
/// Handles the creation of new orders with business rule validation and event dispatching.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPricingService _pricingService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IPricingService pricingService,
        IMapper mapper,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _pricingService = pricingService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Processes the order creation command with validation and business logic.
    /// </summary>
    /// <param name="request">The create order command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A DTO representing the created order.</returns>
    /// <exception cref="CustomerNotFoundException">Thrown when the customer doesn't exist.</exception>
    /// <exception cref="ValidationException">Thrown when order validation fails.</exception>
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating order for customer {CustomerId}", request.CustomerId);

        // Validate customer exists
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer == null)
        {
            throw new CustomerNotFoundException(request.CustomerId);
        }

        // Create order aggregate
        var orderItems = request.Items.Select(item => 
            new OrderItem(item.ProductId, item.Quantity, item.UnitPrice)).ToList();
        
        var order = Order.Create(
            customer.Id,
            orderItems,
            request.ShippingAddress,
            _pricingService);

        // Persist order
        await _orderRepository.AddAsync(order, cancellationToken);

        _logger.LogInformation("Order {OrderId} created successfully", order.Id);

        return _mapper.Map<OrderDto>(order);
    }
}
```

### Application Service

```csharp
namespace YourCompany.YourDomain.Application.Services;

/// <summary>
/// Application service coordinating order-related operations and cross-cutting concerns.
/// </summary>
public class OrderApplicationService : IOrderApplicationService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;
    private readonly IPaymentService _paymentService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderApplicationService> _logger;

    public OrderApplicationService(
        IOrderRepository orderRepository,
        IEmailService emailService,
        IPaymentService paymentService,
        IUnitOfWork unitOfWork,
        ILogger<OrderApplicationService> logger)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
        _paymentService = paymentService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Processes order payment with transaction management and notification.
    /// </summary>
    /// <param name="orderId">The order to process payment for.</param>
    /// <param name="paymentDetails">Payment information and method.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Payment processing result.</returns>
    /// <exception cref="OrderNotFoundException">Thrown when order doesn't exist.</exception>
    /// <exception cref="PaymentProcessingException">Thrown when payment fails.</exception>
    public async Task<PaymentResult> ProcessOrderPaymentAsync(
        OrderId orderId, 
        PaymentDetails paymentDetails, 
        CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new OrderNotFoundException(orderId);
            }

            // Process payment through external service
            var paymentResult = await _paymentService.ProcessPaymentAsync(
                order.TotalAmount, 
                paymentDetails, 
                cancellationToken);

            if (paymentResult.IsSuccessful)
            {
                // Update order status
                order.MarkAsPaid(paymentResult.TransactionId);
                await _orderRepository.UpdateAsync(order, cancellationToken);

                // Send confirmation email
                await _emailService.SendOrderConfirmationAsync(order, cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("Payment processed successfully for order {OrderId}", orderId);
            }
            else
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogWarning("Payment failed for order {OrderId}: {Reason}", orderId, paymentResult.FailureReason);
            }

            return paymentResult;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error processing payment for order {OrderId}", orderId);
            throw;
        }
    }
}
```
