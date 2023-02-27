import { ExpensesList } from "./components/ExpensesList";
import { ExpenseAdd } from "./components/ExpenseAdd";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/expenses-list',
        element: <ExpensesList />
    },
    {
        path: '/expense-add',
        element: <ExpenseAdd />
    }
];

export default AppRoutes;
