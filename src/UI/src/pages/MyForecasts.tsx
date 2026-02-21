import React from 'react';
import { Typography, Box, Paper } from '@mui/material';

const MyForecasts: React.FC = () => {
    return (
        <Box>
            <Typography variant="h4" gutterBottom>My Forecasts</Typography>
            <Paper sx={{ p: 3 }}>
                <Typography variant="body1">Here you will see and manage your forecasts for the upcoming matches.</Typography>
            </Paper>
        </Box>
    );
};

export default MyForecasts;
