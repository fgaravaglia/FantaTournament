import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { Box, Button, Typography, Container, Paper } from '@mui/material';
import { Navigate, useLocation } from 'react-router-dom';

const LoginPage: React.FC = () => {
    const { loginWithRedirect, isAuthenticated, isLoading } = useAuth0();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/";

    if (isLoading) {
        return null; // Handle loading in RequireAuth or App
    }

    if (isAuthenticated) {
        return <Navigate to={from} replace />;
    }

    return (
        <Box
            sx={{
                backgroundColor: 'background.default',
                minHeight: '100vh',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
            }}
        >
            <Container maxWidth="xs">
                <Paper
                    elevation={0}
                    sx={{
                        p: 4,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        backgroundColor: 'background.paper',
                        border: '1px solid #2c2e33',
                        borderRadius: 1,
                    }}
                >
                    <Typography
                        variant="h4"
                        sx={{
                            fontWeight: 'bold',
                            color: 'text.primary',
                            letterSpacing: '4px',
                            mb: 4,
                        }}
                    >
                        CORONA
                    </Typography>
                    <Typography variant="body1" sx={{ color: 'text.secondary', mb: 4, textAlign: 'center' }}>
                        Welcome back! Please sign in to your account.
                    </Typography>
                    <Button
                        fullWidth
                        variant="contained"
                        color="primary"
                        size="large"
                        onClick={() => loginWithRedirect()}
                        sx={{ py: 1.5, fontWeight: 'bold' }}
                    >
                        Sign In
                    </Button>
                </Paper>
            </Container>
        </Box>
    );
};

export default LoginPage;
