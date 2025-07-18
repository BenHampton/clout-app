import {
  isRouteErrorResponse,
  Links,
  Meta,
  Outlet,
  Scripts,
  ScrollRestoration,
} from 'react-router'
import type { Route } from './+types/root'
// import { AppLoader } from './components/loader/AppLoader'
import styles from './root.module.css'
import './styles/vars.css'
import './styles/global.css'
// import { ErrorCard } from './components/errorCard/ErrorCard'


// // full-page loading indicator, hit on page load
// export const HydrateFallback = () => {
//   return <AppLoader />
// }

export function Layout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <head>
        <meta charSet="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <Meta />
        <Links />
      </head>
      <body>
        {children}
        <ScrollRestoration />
        <Scripts />
      </body>
    </html>
  );
}

const App = () => {
  return <Outlet />;
}

export default App;

export function ErrorBoundary({ error }: Route.ErrorBoundaryProps) {
  let message = "Oops!";
  let details = "An unexpected error occurred.";
  let stack: string | undefined;

  if (isRouteErrorResponse(error)) {
    message = error.status === 404 ? "404" : "Error";
    details =
      error.status === 404
        ? "The requested page could not be found."
        : error.statusText || details;
  } else if (import.meta.env.DEV && error && error instanceof Error) {
    details = error.message;
    stack = error.stack;
  }

  return (
    <div className={styles.appErrorWrapper}>
      {/*<ErrorCard title={title} description={description} details={details} />*/}
      TODO Error Cart
    </div>
  );
}
