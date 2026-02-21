# FantaTournament - UI Requirements

The frontend of FantaTournament is a modern web application built with React and TypeScript, following Google's Material Design principles.

## Technical Stack
- **Framework**: React 19 + TypeScript
- **Build Tool**: Vite
- **UI Library**: Material UI (MUI)
- **Routing**: React Router DOM

## Layout Structure
The application features a responsive layout divided into three main sections:

### 1. Fixed Header Bar (Top)
- **Left Section**: Navigation links to static pages.
  - Home
  - Privacy policy
  - Support
  - Regulation
- **Right Section**: User utility links.
  - Notifications Center (with badge)
  - Profile / User Avatar

### 2. Collapsible Sidebar (Left)
Provides access to core application features:
- Home
- My Forecast
- Ranking
- Board
- Leagues
- Logout

### 3. Central Content Area
A dynamic area that renders the page content based on the active route. It is responsive and adjusts margins automatically when the sidebar is toggled.

## Visual Style
- **Aesthetics**: Premium Material Design with a clean, light-mode balanced palette.
- **Responsiveness**: Fully adaptable from desktop to mobile devices using MUI's Grid and Box systems.
- **Typography**: Uses the Roboto font family for a modern look.
