# FantaTournament - UI Requirements

The frontend of FantaTournament is a modern web application built with React and TypeScript, following Google's Material Design principles.

## Technical Stack
- **Framework**: React 19 + TypeScript
- **Build Tool**: Vite
- **UI Library**: Material UI (MUI)
- **Routing**: React Router DOM

## Layout Structure
The application features a responsive, full-width layout divided into three main sections.
The design supports dynamic application titles and branding defined via environment variables (`VITE_APPLICATION_TITLE`).

### 1. Fixed Header Bar (Top)
- **Left Section**: Dynamic Application Title (e.g., "Forecast Cup") and navigation links.
- **Right Section**: User utility links.
  - Notifications Center (with badge)
  - Profile / User Avatar

### 2. Collapsible Sidebar (Left)
Redesigned with modern icons and enhanced structure:
- **Home**: Main dashboard.
- **My Forecast**: User's active forecasts.
- **Ranking**: Global and league standings.
- **Tournament Board**: Active tournament match details.
- **Leagues**: Private and public user groups.
- **Admin Tools**: Collapsible section for administrative tasks.
- **Logout**: Secure session termination.

### 3. Central Content Area (100% Width)
A dynamic area that renders the page content based on the active route. It is responsive and occupies 100% of the available horizontal space.

#### Home Page (3-Zone Layout)
- **Zone A (KPI Headers)**: Summary cards for Global Position, Points, and completeness.
- **Zone B (Main Content - 75%)**: Primary rankings and lists.
- **Zone C (Side Content - 25%)**: Sidebars for specific data like "Next Matches".

## Visual Style
- **Aesthetics**: Premium Material Design with a clean, light-mode balanced palette.
- **Responsiveness**: Fully adaptable from desktop to mobile devices using MUI's Grid and Box systems.
- **Typography**: Uses the Roboto font family for a modern look.
