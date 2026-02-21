import { Routes, Route } from 'react-router-dom';
import MainLayout from './layouts/MainLayout';
import Home from './pages/Home';
import MyForecasts from './pages/MyForecasts';
import Ranking from './pages/Ranking';
import Board from './pages/Board';
import Leagues from './pages/Leagues';
import { Typography, Box } from '@mui/material';

// Simplistic placeholder for other pages
const PlaceholderPage: React.FC<{ title: string }> = ({ title }) => (
  <Box>
    <Typography variant="h4">{title}</Typography>
    <Typography>This page is under construction.</Typography>
  </Box>
);

function App() {
  return (
    <Routes>
      <Route path="/" element={<MainLayout />}>
        <Route index element={<Home />} />
        <Route path="forecasts" element={<MyForecasts />} />
        <Route path="ranking" element={<Ranking />} />
        <Route path="board" element={<Board />} />
        <Route path="leagues" element={<Leagues />} />

        {/* Header Links */}
        <Route path="privacy" element={<PlaceholderPage title="Privacy Policy" />} />
        <Route path="support" element={<PlaceholderPage title="Support" />} />
        <Route path="regulation" element={<PlaceholderPage title="Regulation" />} />

        {/* Profile & Notifications */}
        <Route path="profile" element={<PlaceholderPage title="Profile" />} />
        <Route path="notifications" element={<PlaceholderPage title="Notifications" />} />
      </Route>
    </Routes>
  );
}

export default App;
