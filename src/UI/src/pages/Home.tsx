import React from 'react';
import { Typography, Paper, Box, Grid, Card, CardContent } from '@mui/material';

const Home: React.FC = () => {
    return (
        <Box>
            <Typography variant="h4" gutterBottom>
                Welcome to FantaTournament
            </Typography>
            <Typography variant="subtitle1" color="textSecondary" gutterBottom>
                Manage your forecasts and compete with other players for the glory!
            </Typography>

            <Grid container spacing={3} sx={{ mt: 2 }}>
                <Grid size={{ xs: 12, md: 8 }}>
                    <Paper sx={{ p: 3, borderRadius: 2 }}>
                        <Typography variant="h6">Latest Results</Typography>
                        <Typography variant="body2" color="textSecondary">
                            No results to show at the moment.
                        </Typography>
                    </Paper>
                </Grid>
                <Grid size={{ xs: 12, md: 4 }}>
                    <Card sx={{ borderRadius: 2 }}>
                        <CardContent>
                            <Typography variant="h6" color="primary">Your Statistics</Typography>
                            <Typography variant="body2">Points: 0</Typography>
                            <Typography variant="body2">Rank: #--</Typography>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </Box>
    );
};

export default Home;
