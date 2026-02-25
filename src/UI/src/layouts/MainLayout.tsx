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
    Inventory as ForecastIcon,
    TableChart as LeaguesIcon,
    SportsSoccer as BoardIcon,
    Settings as AdminIcon,
    ExitToApp as LogoutIcon,
    Notifications as NotificationsIcon,
    Person as PersonIcon,
    ExpandLess,
    ExpandMore
} from '@mui/icons-material';
import { Collapse } from '@mui/material';
import { Link, Outlet } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';

const drawerWidth = 280;

const MainLayout: React.FC = () => {
    const { user, logout } = useAuth0();
    const [isSidebarOpen, setIsSidebarOpen] = useState(true);

    const handleDrawerToggle = () => {
        setIsSidebarOpen(!isSidebarOpen);
    };

    const [isAdminOpen, setIsAdminOpen] = useState(false);

    const handleAdminToggle = () => {
        setIsAdminOpen(!isAdminOpen);
    };

    const menuItems = [
        { text: 'Home', icon: <HomeIcon />, path: '/', color: '#10b981' }, // Green
        { text: 'My Forecast', icon: <ForecastIcon />, path: '/forecasts', color: '#f59e0b' }, // Amber
        { text: 'Ranking', icon: <RankingIcon />, path: '/ranking', color: '#ec4899' }, // Pink
        { text: 'Tournament Board', icon: <BoardIcon />, path: '/board', color: '#06b6d4' }, // Cyan
        { text: 'Leagues', icon: <LeaguesIcon />, path: '/leagues', color: '#8b5cf6' }, // Violet
    ];

    const adminItems = [
        { text: 'Users', path: '/admin/users' },
        { text: 'Settings', path: '/admin/settings' },
    ];

    const headerLinks = [
        { text: 'Home', path: '/' },
        { text: 'Privacy policy', path: '/privacy' },
        { text: 'Support', path: '/support' },
        { text: 'Regulation', path: '/regulation' },
    ];

    const drawer = (
        <div>
            <Toolbar sx={{ display: 'flex', alignItems: 'center', px: 2 }}>
                <Typography variant="h5" sx={{ fontWeight: 'bold', color: 'text.primary', letterSpacing: '2px' }}>
                    {import.meta.env.VITE_APPLICATION_TITLE}
                </Typography>
            </Toolbar>
            <Box sx={{ px: 2, py: 3, display: 'flex', alignItems: 'center' }}>
                <Avatar
                    src={user?.picture}
                    sx={{ width: 40, height: 40, mr: 2, bgcolor: 'primary.main' }}
                >
                    {user?.name?.charAt(0) || 'U'}
                </Avatar>
                <Box sx={{ overflow: 'hidden' }}>
                    <Typography
                        variant="subtitle2"
                        sx={{ fontWeight: 'bold', color: 'text.primary', whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}
                    >
                        {user?.name || 'User'}
                    </Typography>
                    <Typography variant="caption" sx={{ color: 'text.secondary' }}>
                        {user?.email || 'Authenticated'}
                    </Typography>
                </Box>
                <IconButton size="small" sx={{ ml: 'auto', color: 'text.secondary' }}>
                    <MenuIcon fontSize="small" />
                </IconButton>
            </Box>
            <Typography variant="overline" sx={{ px: 3, py: 1, display: 'block', color: 'text.secondary', fontWeight: 'bold' }}>
                Navigation
            </Typography>
            <List sx={{ px: 1 }}>
                {menuItems.map((item) => (
                    <ListItem key={item.text} disablePadding sx={{ mb: 0.5 }}>
                        <ListItemButton
                            component={Link}
                            to={item.path}
                            sx={{
                                borderRadius: 1,
                                '& .MuiListItemIcon-root': { color: item.color, minWidth: 48 }
                            }}
                        >
                            <ListItemIcon>
                                <Box sx={{
                                    width: 32,
                                    height: 32,
                                    borderRadius: '8px',
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center',
                                    bgcolor: 'rgba(255, 255, 255, 0.05)',
                                    border: '1px solid rgba(255, 255, 255, 0.1)'
                                }}>
                                    {React.cloneElement(item.icon as React.ReactElement, { sx: { fontSize: 20 } })}
                                </Box>
                            </ListItemIcon>
                            <ListItemText
                                primary={item.text}
                                primaryTypographyProps={{ variant: 'body2', sx: { fontWeight: 500, color: 'text.primary' } }}
                            />
                        </ListItemButton>
                    </ListItem>
                ))}

                {/* Admin Tools Collapsible */}
                <ListItem disablePadding sx={{ mb: 0.5 }}>
                    <ListItemButton
                        onClick={handleAdminToggle}
                        sx={{
                            borderRadius: 1,
                            '& .MuiListItemIcon-root': { color: '#f97316', minWidth: 48 } // Orange for Admin
                        }}
                    >
                        <ListItemIcon>
                            <Box sx={{
                                width: 32,
                                height: 32,
                                borderRadius: '8px',
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center',
                                bgcolor: 'rgba(255, 255, 255, 0.05)',
                                border: '1px solid rgba(255, 255, 255, 0.1)'
                            }}>
                                <AdminIcon sx={{ fontSize: 20 }} />
                            </Box>
                        </ListItemIcon>
                        <ListItemText
                            primary="Admin Tools"
                            primaryTypographyProps={{ variant: 'body2', sx: { fontWeight: 500, color: 'text.primary' } }}
                        />
                        {isAdminOpen ? <ExpandLess sx={{ color: 'text.secondary' }} /> : <ExpandMore sx={{ color: 'text.secondary' }} />}
                    </ListItemButton>
                </ListItem>
                <Collapse in={isAdminOpen} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding sx={{ pl: 4 }}>
                        {adminItems.map((item) => (
                            <ListItemButton
                                key={item.text}
                                component={Link}
                                to={item.path}
                                sx={{ borderRadius: 1, mb: 0.5 }}
                            >
                                <ListItemText
                                    primary={item.text}
                                    primaryTypographyProps={{ variant: 'body2' }}
                                />
                            </ListItemButton>
                        ))}
                    </List>
                </Collapse>

                <Divider sx={{ my: 1, borderColor: '#2c2e33' }} />

                <ListItem disablePadding>
                    <ListItemButton
                        onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
                        sx={{
                            borderRadius: 1,
                            '& .MuiListItemIcon-root': { color: '#94a3b8', minWidth: 48 } // Grey for logout
                        }}
                    >
                        <ListItemIcon>
                            <Box sx={{
                                width: 32,
                                height: 32,
                                borderRadius: '8px',
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center',
                                bgcolor: 'rgba(255, 255, 255, 0.05)',
                                border: '1px solid rgba(255, 255, 255, 0.1)'
                            }}>
                                <LogoutIcon sx={{ fontSize: 20 }} />
                            </Box>
                        </ListItemIcon>
                        <ListItemText
                            primary="Logout"
                            primaryTypographyProps={{ variant: 'body2', sx: { fontWeight: 500, color: 'text.primary' } }}
                        />
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
                    width: isSidebarOpen ? `calc(100% - ${drawerWidth}px)` : '100%',
                    ml: isSidebarOpen ? `${drawerWidth}px` : 0,
                    transition: (theme) => theme.transitions.create(['margin', 'width'], {
                        easing: theme.transitions.easing.sharp,
                        duration: theme.transitions.duration.leavingScreen,
                    }),
                    zIndex: (theme) => theme.zIndex.drawer + 1,
                    backgroundColor: 'background.paper',
                    color: 'text.primary',
                    boxShadow: 'none',
                    borderBottom: '1px solid #2c2e33'
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

                    <Typography variant="h5" noWrap component="div" sx={{ fontWeight: 'bold', color: 'text.primary', mr: 4, letterSpacing: '1px' }}>
                        {import.meta.env.VITE_APPLICATION_TITLE}
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
                            <Avatar
                                src={user?.picture}
                                sx={{ width: 32, height: 32, bgcolor: 'secondary.main' }}
                            >
                                {user?.name?.charAt(0) || <PersonIcon />}
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

            <Box
                component="main"
                sx={{
                    flexGrow: 1,
                    p: 3,
                    display: 'flex',
                    flexDirection: 'column',
                    minHeight: '100vh',
                    backgroundColor: 'background.default',
                    transition: (theme) => theme.transitions.create('margin', {
                        easing: theme.transitions.easing.sharp,
                        duration: theme.transitions.duration.leavingScreen,
                    }),
                }}
            >
                <Toolbar />
                <Box sx={{ flexGrow: 1 }}>
                    <Outlet />
                </Box>
                <Box
                    component="footer"
                    sx={{
                        py: 2,
                        mt: 4,
                        borderTop: '1px solid #2c2e33',
                        display: 'flex',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                    }}
                >
                    <Typography variant="caption" color="text.secondary">
                        Copyright © 2022-2026. All rights reserved.
                    </Typography>
                    <Typography variant="caption" color="text.secondary">
                        FG Solutions - vers. {import.meta.env.VITE_APP_VERSION}
                    </Typography>
                </Box>
            </Box>
        </Box>
    );
};

export default MainLayout;
