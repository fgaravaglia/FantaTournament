import React from 'react';
import { Typography, Paper, Box, Grid, Card, CardContent, Stack, Divider, Button } from '@mui/material';
import {
    BarChart as BarChartIcon,
    Calculate as CalculatorIcon,
    Speed as GaugeIcon,
    NavigateNext as MoreIcon,
    EmojiEvents as RankingIcon
} from '@mui/icons-material';

const Home: React.FC = () => {
    return (
        <Box sx={{ flexGrow: 1, width: '100%' }}>
            {/* Zone A: KPI Widgets */}
            <Grid container spacing={2} sx={{ mb: 4, width: '100%', marginX: 0 }}>
                <Grid item xs={12} md={4} sx={{ display: 'flex', width: '33%' }}>
                    <Paper sx={{ bgcolor: 'background.paper', border: '1px solid #2c2e33', borderRadius: 1, p: 2, flexGrow: 1 }}>
                        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                            <Box>
                                <Typography variant="h5" sx={{ fontWeight: 'bold', color: 'text.primary' }}>2 / 8</Typography>
                                <Typography variant="caption" sx={{ color: 'success.main', fontWeight: 'bold', display: 'block', mt: 0.5 }}>+3.5%</Typography>
                                <Typography variant="body2" sx={{ color: 'text.secondary', mt: 1 }}>My Global Position</Typography>
                            </Box>
                            <Box sx={{
                                bgcolor: 'rgba(16, 185, 129, 0.1)',
                                p: 1,
                                borderRadius: 1,
                                display: 'flex'
                            }}>
                                <BarChartIcon sx={{ color: '#10b981', fontSize: 24 }} />
                            </Box>
                        </Box>
                    </Paper>
                </Grid>
                <Grid item xs={12} md={4} sx={{ display: 'flex', width: '32%' }}>
                    <Paper sx={{ bgcolor: 'background.paper', border: '1px solid #2c2e33', borderRadius: 1, p: 2, flexGrow: 1 }}>
                        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                            <Box>
                                <Typography variant="h5" sx={{ fontWeight: 'bold', color: 'text.primary' }}>179 Points</Typography>
                                <Typography variant="caption" sx={{ color: 'success.main', fontWeight: 'bold', display: 'block', mt: 0.5 }}>+11%</Typography>
                                <Typography variant="body2" sx={{ color: 'text.secondary', mt: 1 }}>My Forecast - Last Update: 22 Jul 2024</Typography>
                            </Box>
                            <Box sx={{
                                bgcolor: 'rgba(245, 158, 11, 0.1)',
                                p: 1,
                                borderRadius: 1,
                                display: 'flex'
                            }}>
                                <CalculatorIcon sx={{ color: '#f59e0b', fontSize: 24 }} />
                            </Box>
                        </Box>
                    </Paper>
                </Grid>
                <Grid item xs={12} md={4} sx={{ display: 'flex', width: '32.5%' }}>
                    <Paper sx={{ bgcolor: 'background.paper', border: '1px solid #2c2e33', borderRadius: 1, p: 2, flexGrow: 1 }}>
                        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                            <Box>
                                <Typography variant="h5" sx={{ fontWeight: 'bold', color: 'text.primary' }}>50 / 51 Matches</Typography>
                                <Typography variant="caption" sx={{ color: 'error.main', fontWeight: 'bold', display: 'block', mt: 0.5 }}>-2.4%</Typography>
                                <Typography variant="body2" sx={{ color: 'text.secondary', mt: 1 }}>Completeness of my Forecast</Typography>
                            </Box>
                            <Box sx={{
                                bgcolor: 'rgba(236, 72, 153, 0.1)',
                                p: 1,
                                borderRadius: 1,
                                display: 'flex'
                            }}>
                                <GaugeIcon sx={{ color: '#ec4899', fontSize: 24 }} />
                            </Box>
                        </Box>
                    </Paper>
                </Grid>
            </Grid>

            {/* Zone B (75%) & Zone C (25%) */}
            <Grid container spacing={3}>
                {/* Zone B: Main Content (75%) */}
                <Grid item xs={12} md={9} sx={{ width: '75%' }}>
                    <Stack spacing={3}>
                        <Paper sx={{ borderRadius: 1, overflow: 'hidden' }}>
                            <Box sx={{ p: 2, bgcolor: 'background.paper', borderBottom: '1px solid #2c2e33' }}>
                                <Typography variant="subtitle1" sx={{ fontWeight: 'bold' }}>World Wide League - Podium</Typography>
                            </Box>
                            <Box sx={{ p: 2 }}>
                                <Typography variant="body2" color="textSecondary" sx={{ py: 4, textAlign: 'center' }}>
                                    Ranking table will be displayed here.
                                </Typography>
                            </Box>
                        </Paper>

                        <Paper sx={{ borderRadius: 1, overflow: 'hidden' }}>
                            <Box sx={{ p: 2, bgcolor: 'background.paper', borderBottom: '1px solid #2c2e33' }}>
                                <Typography variant="subtitle1" sx={{ fontWeight: 'bold' }}>My Leagues</Typography>
                            </Box>
                            <Box sx={{ p: 2 }}>
                                <Typography variant="body2" color="textSecondary" sx={{ py: 4, textAlign: 'center' }}>
                                    Your joined leagues will be listed here.
                                </Typography>
                            </Box>
                        </Paper>
                    </Stack>
                </Grid>

                {/* Zone C: Side Content (25%) */}
                <Grid item xs={12} md={3} sx={{ width: '23%' }}>
                    <Stack spacing={2}>
                        <Card sx={{ borderRadius: 1 }}>
                            <CardContent sx={{ p: 2, display: 'flex', alignItems: 'center' }}>
                                <Box sx={{ bgcolor: 'secondary.main', p: 1, borderRadius: 1, mr: 2, display: 'flex' }}>
                                    <RankingIcon sx={{ color: 'white' }} />
                                </Box>
                                <Box>
                                    <Typography variant="caption" sx={{ display: 'block', color: 'text.secondary' }}>World Wide League</Typography>
                                    <Typography variant="h6" sx={{ lineHeight: 1 }}>2</Typography>
                                </Box>
                            </CardContent>
                        </Card>

                        <Paper sx={{ borderRadius: 1, overflow: 'hidden' }}>
                            <Box sx={{ p: 1.5, bgcolor: 'background.paper', borderBottom: '1px solid #2c2e33' }}>
                                <Typography variant="body2" sx={{ fontWeight: 'bold' }}>Next Matches</Typography>
                            </Box>
                            <Box sx={{ p: 2, textAlign: 'center' }}>
                                <Typography variant="caption" color="textSecondary">No Match Scheduled yet</Typography>
                            </Box>
                        </Paper>
                    </Stack>
                </Grid>
            </Grid>
        </Box>
    );
};

export default Home;
