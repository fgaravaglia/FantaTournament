import React, { useState } from 'react';
import {
    AppBar,
    Box,
    CssBaseline,
    Divider,
    Drawer,
    IconButton,
    List,
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Toolbar,
    Typography,
    Badge,
    Avatar,
    Stack,
    Button
} from '@mui/material';
import {
    Menu as MenuIcon,
    Home as HomeIcon,
    EmojiEvents as RankingIcon,
    Assessment as BoardIcon,
    Groups as LeaguesIcon,
    History as ForecastIcon,
    Logout as LogoutIcon,
    Notifications as NotificationsIcon,
    Person as PersonIcon
} from '@mui/icons-material';
import { Link, Outlet } from 'react-router-dom';

const drawerWidth = 240;

const MainLayout: React.FC = () => {
    const [isSidebarOpen, setIsSidebarOpen] = useState(true);

    const handleDrawerToggle = () => {
        setIsSidebarOpen(!isSidebarOpen);
    };

    const menuItems = [
        { text: 'Home', icon: <HomeIcon />, path: '/' },
        { text: 'My Forecast', icon: <ForecastIcon />, path: '/forecasts' },
        { text: 'Ranking', icon: <RankingIcon />, path: '/ranking' },
        { text: 'Board', icon: <BoardIcon />, path: '/board' },
        { text: 'Leagues', icon: <LeaguesIcon />, path: '/leagues' },
    ];

    const headerLinks = [
        { text: 'Home', path: '/' },
        { text: 'Privacy policy', path: '/privacy' },
        { text: 'Support', path: '/support' },
        { text: 'Regulation', path: '/regulation' },
    ];

    const drawer = (
        <div>
            <Toolbar />
            <Divider />
            <List>
                {menuItems.map((item) => (
                    <ListItem key={item.text} disablePadding>
                        <ListItemButton component={Link} to={item.path}>
                            <ListItemIcon>{item.icon}</ListItemIcon>
                            <ListItemText primary={item.text} />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
            <Divider />
            <List>
                <ListItem disablePadding>
                    <ListItemButton onClick={() => console.log('Logout')}>
                        <ListItemIcon><LogoutIcon /></ListItemIcon>
                        <ListItemText primary="Logout" />
                    </ListItemButton>
                </ListItem>
            </List>
        </div>
    );

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />

            {/* Header Bar */}
            <AppBar
                position="fixed"
                sx={{
                    zIndex: (theme) => theme.zIndex.drawer + 1,
                    backgroundColor: '#fff',
                    color: '#333',
                    boxShadow: '0 2px 4px rgba(0,0,0,0.1)'
                }}
            >
                <Toolbar>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        edge="start"
                        onClick={handleDrawerToggle}
                        sx={{ mr: 2 }}
                    >
                        <MenuIcon />
                    </IconButton>

                    <Typography variant="h6" noWrap component="div" sx={{ fontWeight: 'bold', color: '#1976d2', mr: 4 }}>
                        FantaTournament
                    </Typography>

                    {/* Top Nav Links */}
                    <Stack direction="row" spacing={2} sx={{ display: { xs: 'none', md: 'flex' }, flexGrow: 1 }}>
                        {headerLinks.map((link) => (
                            <Button key={link.text} color="inherit" component={Link} to={link.path}>
                                {link.text}
                            </Button>
                        ))}
                    </Stack>

                    <Box sx={{ flexGrow: 1, display: { md: 'none' } }} />

                    {/* Right side Icons */}
                    <Stack direction="row" spacing={1} alignItems="center">
                        <IconButton color="inherit" component={Link} to="/notifications">
                            <Badge badgeContent={4} color="error">
                                <NotificationsIcon />
                            </Badge>
                        </IconButton>
                        <IconButton color="inherit" component={Link} to="/profile">
                            <Avatar sx={{ width: 32, height: 32, bgcolor: '#1976d2' }}>
                                <PersonIcon />
                            </Avatar>
                        </IconButton>
                    </Stack>
                </Toolbar>
            </AppBar>

            {/* Sidebar Navigation */}
            <Drawer
                variant="persistent"
                open={isSidebarOpen}
                sx={{
                    width: isSidebarOpen ? drawerWidth : 0,
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: drawerWidth,
                        boxSizing: 'border-box',
                    },
                }}
            >
                {drawer}
            </Drawer>

            {/* Main Content Area */}
            <Box
                component="main"
                sx={{
                    flexGrow: 1,
                    p: 3,
                    backgroundColor: '#f4f6f8',
                    minHeight: '100vh',
                    transition: (theme) => theme.transitions.create('margin', {
                        easing: theme.transitions.easing.sharp,
                        duration: theme.transitions.duration.leavingScreen,
                    }),
                    marginLeft: isSidebarOpen ? 0 : `-${drawerWidth}px`,
                }}
            >
                <Toolbar />
                <Outlet />
            </Box>
        </Box>
    );
};

export default MainLayout;
