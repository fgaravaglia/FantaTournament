import React from 'react';
import { Typography, Box, Paper } from '@mui/material';

const Board: React.FC = () => {
    return (
        <Box>
            <Typography variant="h4" gutterBottom>Tournament Board</Typography>
            <Paper sx={{ p: 3 }}>
                <Typography variant="body1">View the tournament structure, matches, and team progress.</Typography>
            </Paper>
        </Box>
    );
};

export default Board;
