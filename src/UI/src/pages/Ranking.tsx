import React from 'react';
import { Typography, Box, Paper } from '@mui/material';

const Ranking: React.FC = () => {
    return (
        <Box>
            <Typography variant="h4" gutterBottom>Ranking</Typography>
            <Paper sx={{ p: 3 }}>
                <Typography variant="body1">Check your position in the global ranking and leagues.</Typography>
            </Paper>
        </Box>
    );
};

export default Ranking;
