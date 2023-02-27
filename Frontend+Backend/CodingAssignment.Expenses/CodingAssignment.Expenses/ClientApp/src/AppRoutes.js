import { ExpenseAdd } from "./components/ExpenseAdd";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/expense-add',
        element: <ExpenseAdd />
    }
];

export default AppRoutes;
