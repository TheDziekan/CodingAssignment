import React, { Component } from 'react';

export class ExpensesList extends Component {
    static displayName = ExpensesList.name;

    constructor(props) {
        super(props);
        this.state = { expenses: [], loading: true };

        this.controller = new AbortController();
        this.signal = this.controller.signal;
    }

    componentDidMount() {
        this.populateExpensesList();
    }

    componentWillUnmount() {
        this.controller.abort();
    }

    static renderForecastsTable(expenses) {
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Transaction Date</th>
                        <th>Recipient</th>
                        <th>Amount</th>
                        <th>Currency</th>
                        <th>Type</th>
                    </tr>
                </thead>
                <tbody>
                    {expenses.map(expense =>
                        <tr key={expense.id}>
                            <td>{expense.transactionDate}</td>
                            <td>{expense.recipient}</td>
                            <td>{expense.amount}</td>
                            <td>{expense.currency}</td>
                            <td>{expense.expenseType}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ExpensesList.renderForecastsTable(this.state.expenses);

        return (
            <div>
                <h1 id="tableLabel">Expenses</h1>
                {contents}
            </div>
        );
    }

    async populateExpensesList() {
        const response = await fetch('expense', { signal: this.signal });
        const data = await response.json();
        this.setState({ expenses: data, loading: false });
    }
}