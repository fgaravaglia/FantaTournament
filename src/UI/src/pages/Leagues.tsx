import React from 'react';
import { Typography, Box, Paper } from '@mui/material';

const Leagues: React.FC = () => {
    return (
        <Box>
            <Typography variant="h4" gutterBottom>Leagues</Typography>
            <Paper sx={{ p: 3 }}>
                <Typography variant="body1">Join or create private leagues to compete with friends.</Typography>
            </Paper>
        </Box>
    );
};

export default Leagues;
