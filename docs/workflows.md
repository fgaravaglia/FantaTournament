# Workflows

This document explain main workflows in this repository.

exmaples

## Adding a New API Endpoint

1. Check if similar endpoint exists in Api layer (`src/Fantatournament.Api/`
2. Create schema in `src/schemas/` if new data types needed
3. Implement endpoint in appropriate router file
4. Add tests in `tests/api/`
5. Update API documentation
6. Run full test suite before committing

## Database Schema Changes

1. Describe the change and why it's needed
2. Create migration: `alembic revision --autogenerate -m "description"`
3. Review generated migration file
4. Test migration: `alembic upgrade head`
5. Test rollback: `alembic downgrade -1`
6. Update relevant models and schemas
