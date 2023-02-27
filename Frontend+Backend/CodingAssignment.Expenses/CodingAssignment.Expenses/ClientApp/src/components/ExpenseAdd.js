import React, { Component } from 'react';

export class ExpenseAdd extends Component {
    static displayName = ExpenseAdd.name;

    constructor(props) {
        super(props);
        this.state = { currencies: [], expenseTypes: [], loading: true };

        this.controller = new AbortController();
        this.signal = this.controller.signal;

        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        this.populateSelectLists();
    }

    componentWillUnmount() {
        this.controller.abort();
    }

    async handleSubmit(event) {
        event.preventDefault();
        const data = {
            transactionDate: event.target[0].value,
            recipient: event.target[1].value,
            amount: event.target[2].value,
            currencyId: event.target[3].value,
            expenseTypeId: event.target[4].value,
        }

        await fetch('expense', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
            signal: this.signal
        });

        document.getElementById("addExpenseForm").reset();
        alert("Submitted successfully!");
    }

    render() {
        return (
            <div>
                <div>
                    <h1 id="tableLabel">Add Expense</h1>
                </div>
                <form id="addExpenseForm" onSubmit={this.handleSubmit}>
                    <div className="form-floating mb-3">
                        <input type="date" className="form-control" id="dateInput" required></input>
                        <label htmlFor="dateInput">Transaction Date</label>
                    </div>
                    <div className="form-floating mb-3">
                        <input type="text" className="form-control" id="recipientInput" placeholder="Recipient" required></input>
                        <label htmlFor="recipientInput">Recipient</label>
                    </div>
                    <div className="form-floating mb-3">
                        <input type="number" step="0.01" min="0.01" className="form-control" id="amountInput" placeholder="Amount" required></input>
                        <label htmlFor="amountInput">Amount</label>
                    </div>
                    <select className="form-select mb-3">
                        {this.state.currencies.map(currency =>
                            <option key={currency.id} value={currency.id}>{currency.name}</option>)
                        }
                    </select>
                    <select className="form-select mb-3">
                        {this.state.expenseTypes.map(expenseType =>
                            <option key={expenseType.id} value={expenseType.id}>{expenseType.name}</option>)
                        }
                    </select>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>
            </div>
        );
    }

    async populateSelectLists() {
        const currencyResponse = await fetch('currency', { signal: this.signal });
        const typeResponse = await fetch('expenseType', { signal: this.signal });
        const currencies = await currencyResponse.json();
        const expenseTypes = await typeResponse.json();
        this.setState({ currencies: currencies, expenseTypes: expenseTypes, loading: false });
    }
}