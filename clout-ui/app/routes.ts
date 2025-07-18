import { type RouteConfig, index, route, layout } from '@react-router/dev/routes'

// export default [index("routes/home.tsx")] satisfies RouteConfig;
export default [
  index('routes/home.tsx'),
  // layout('routes/permissionHandler.tsx', [
  //     index('routes/home.tsx'),
  route('login', 'pages/login/Login.tsx'),
  route('user/:userId', 'pages/user/User.tsx'),
  route('search', 'pages/search/Search.tsx'),
  // route('pipeline/:pipelineId/schedule/:scheduleId', 'pages/login/Login.tsx'),
  // route('login', 'routes/login.tsx', [route('test', 'pages/login/Login.tsx')]),
  // ]),

  // layout('routes/permissionHandler.tsx', [
  //     index('routes/home.tsx'),
  //     route('scheduling', 'routes/schedule.tsx', [
  //         index('routes/UnselectedSchedule.tsx'),
  //         route('pipeline/:pipelineId/schedule/:scheduleId', 'pages/schedule/Schedule.tsx'),
  //     ]),
  // ]),
] satisfies RouteConfig
