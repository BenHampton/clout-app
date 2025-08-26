import { type RouteConfig, index, route } from '@react-router/dev/routes'

export default [
  // layout('routes/permissionHandler.tsx', [
  route('/', 'routes/login.tsx', [
    index('pages/Login/Login.tsx'),
    route('/:userName', 'routes/home.tsx', [
      index('pages/Home/Home.tsx'),
      route('profile', 'pages/Profile/Profile.tsx'),
      route('user/:userId', 'pages/User/User.tsx'),
      // route('search', 'pages/search/Search.tsx'),
    ]),
  ]),
  // ]),
] satisfies RouteConfig
