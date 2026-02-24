import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#00d25b', // Corona Green
    },
    secondary: {
      main: '#8f5fe8', // Corona Purple
    },
    background: {
      default: '#0c0d10', // Deep dark background
      paper: '#191c24',   // Card/Sidebar background
    },
    info: {
      main: '#0090e7',
    },
    success: {
      main: '#00d25b',
    },
    warning: {
      main: '#ffab00',
    },
    error: {
      main: '#fc424a',
    },
    text: {
      primary: '#ffffff',
      secondary: '#6c7293',
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
    h1: {
      fontSize: '2.5rem',
      fontWeight: 500,
    },
    h2: {
      fontSize: '2rem',
      fontWeight: 500,
    },
    h3: {
      fontSize: '1.75rem',
      fontWeight: 500,
    },
    h4: {
      fontSize: '1.5rem',
      fontWeight: 500,
    },
    h5: {
      fontSize: '1.25rem',
      fontWeight: 500,
    },
    h6: {
      fontSize: '1.1rem',
      fontWeight: 500,
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          borderRadius: 4,
          fontWeight: 500,
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          backgroundColor: '#191c24',
          backgroundImage: 'none',
          borderRadius: 4,
        },
      },
    },
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundColor: '#191c24',
          backgroundImage: 'none',
          boxShadow: 'none',
          borderBottom: '1px solid #2c2e33',
        },
      },
    },
    MuiDrawer: {
      styleOverrides: {
        paper: {
          backgroundColor: '#191c24',
          borderRight: '1px solid #2c2e33',
        },
      },
    },
    MuiListItemButton: {
      styleOverrides: {
        root: {
          '&.Mui-selected': {
            backgroundColor: 'rgba(143, 95, 232, 0.1)',
            '& .MuiListItemIcon-root': {
              color: '#8f5fe8',
            },
          },
          '&:hover': {
            backgroundColor: 'rgba(255, 255, 255, 0.05)',
          },
        },
      },
    },
  },
});

export default theme;
