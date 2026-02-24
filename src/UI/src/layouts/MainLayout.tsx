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
import { useAuth0 } from '@auth0/auth0-react';

const drawerWidth = 240;

const MainLayout: React.FC = () => {
    const { user, logout } = useAuth0();
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
            <Toolbar sx={{ display: 'flex', alignItems: 'center', px: 2 }}>
                <Typography variant="h5" sx={{ fontWeight: 'bold', color: 'text.primary', letterSpacing: '2px' }}>
                    CORONA
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
                                '& .MuiListItemIcon-root': { color: 'secondary.main', minWidth: 40 }
                            }}
                        >
                            <ListItemIcon>{item.icon}</ListItemIcon>
                            <ListItemText
                                primary={item.text}
                                primaryTypographyProps={{ variant: 'body2', sx: { fontWeight: 500 } }}
                            />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
            <Divider sx={{ my: 1, borderColor: '#2c2e33' }} />
            <Typography variant="overline" sx={{ px: 3, py: 1, display: 'block', color: 'text.secondary', fontWeight: 'bold' }}>
                More
            </Typography>
            <List sx={{ px: 1 }}>
                <ListItem disablePadding>
                    <ListItemButton
                        onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
                        sx={{
                            borderRadius: 1,
                            '& .MuiListItemIcon-root': { color: 'error.main', minWidth: 40 }
                        }}
                    >
                        <ListItemIcon><LogoutIcon /></ListItemIcon>
                        <ListItemText
                            primary="Logout"
                            primaryTypographyProps={{ variant: 'body2', sx: { fontWeight: 500 } }}
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
                        CORONA
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

            {/* Main Content Area */}
            <Box
                component="main"
                sx={{
                    flexGrow: 1,
                    p: 3,
                    backgroundColor: 'background.default',
                    minHeight: '100vh',
                    transition: (theme) => theme.transitions.create('margin', {
                        easing: theme.transitions.easing.sharp,
                        duration: theme.transitions.duration.leavingScreen,
                    }),
                }}
            >
                <Toolbar />
                <Outlet />
            </Box>
        </Box>
    );
};

export default MainLayout;
