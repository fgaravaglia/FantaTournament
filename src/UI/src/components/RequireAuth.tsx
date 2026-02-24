import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { Navigate, useLocation } from 'react-router-dom';
import { Box, CircularProgress } from '@mui/material';

interface RequireAuthProps {
    children: React.ReactNode;
}

const RequireAuth: React.FC<RequireAuthProps> = ({ children }) => {
    const { isAuthenticated, isLoading } = useAuth0();
    const location = useLocation();

    if (isLoading) {
        return (
            <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                <CircularProgress color="primary" />
            </Box>
        );
    }

    if (!isAuthenticated) {
        // Redirect to login page, saving the location they were trying to go to
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    return <>{children}</>;
};

export default RequireAuth;
