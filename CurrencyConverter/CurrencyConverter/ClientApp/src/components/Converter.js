import React, { Component } from 'react';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";

export class Converter extends Component {
	static displayName = Converter.name;

	constructor(props) {
		super(props);
		this.state = {
			date: new Date(),
			amount: 1,
			mostUsedCurrency: "---",
			sourceCurrency: "---",
			destinationCurrency: "HRK",
			allCurrencies: [],
			convertedValue: ""
		};
		this.handleDateChange = this.handleDateChange.bind(this);
		this.handleAmountChange = this.handleAmountChange.bind(this);
		this.setSourceCurrency = this.setSourceCurrency.bind(this);
		this.setDestinationCurrency = this.setDestinationCurrency.bind(this);
		this.doConversion = this.doConversion.bind(this);
		this.switchCurrencies = this.switchCurrencies.bind(this);
		this.reset = this.reset.bind(this);
	}

	componentDidMount() {
		fetch('/Rates/GetCurrencies')
			.then(response => response.json())
			.then(data => this.setState({
				allCurrencies: data
			}));

		fetch('/Rates/GetMostUsedCurrency')
			.then(response => response.text())
			.then(data => this.setState({
				sourceCurrency: data,
				mostUsedCurrency: data
			}));
	}

	handleDateChange(date) {
		this.setState({
			date
		});
	}

	handleAmountChange(event) {
		this.setState({
			amount: event.target.value
		});
	}

	setSourceCurrency(event) {
		this.setState({
			sourceCurrency: event.target.value
		});
	}

	setDestinationCurrency(event) {
		this.setState({
			destinationCurrency: event.target.value
		});
	}

	doConversion() {
		fetch(`/Rates/GetRate?sourceCurrency=${this.state.sourceCurrency}&destinationCurrency=${this.state.destinationCurrency}&rateDate=${this.state.date.toISOString()}&amount=${this.state.amount}`)
			.then(response => response.json())
			.then(data => this.setState({
				convertedValue: data
			}));

		fetch(`/Rates/StoreStats?currencyName=${this.state.sourceCurrency}&conversionDate=${(new Date()).toISOString()}`);
	}

	switchCurrencies() {
		this.setState({
			sourceCurrency: this.state.destinationCurrency,
			destinationCurrency: this.state.sourceCurrency
		});
	}

	reset() {
		this.setState({
			date: new Date(),
			amount: 1,
			sourceCurrency: this.state.mostUsedCurrency,
			destinationCurrency: "HRK",
			allCurrencies: this.state.allCurrencies,
			convertedValue: ""
		});
	}

	render() {
		var options = ["---", ...this.state.allCurrencies];
		var sourceOptions = options.map(c => <option key={c} value={c}>{c}</option>);
		var destinationOptions = this.state.allCurrencies.map(c => <option key={c} value={c}>{c}</option>);

		return (
			<div>
				<h1>Converter</h1>
				<br />

				<button onClick={this.reset}>Reset</button>
				<br />
				<br />

				<div>
					<DatePicker
						maxDate={new Date()}
						selected={this.state.date}
						onChange={this.handleDateChange}
					/>
				</div>
				<br />

				<div>
					<input onChange={this.handleAmountChange}
						value={this.state.amount}
						step="0.1"
						min="0"
						type="number"
					/>

					<select value={this.state.sourceCurrency} onChange={this.setSourceCurrency} >
						{sourceOptions}
					</select>
				</div>
				<br />

				<button onClick={this.switchCurrencies}>Switch</button>
				<br/>
				<br/>

				<div>
					<input disabled value={this.state.convertedValue} type="text"></input>

					<select value={this.state.destinationCurrency} onChange={this.setDestinationCurrency} >
						{destinationOptions}
					</select>
				</div>
				<br />

				<button onClick={this.doConversion}>Convert</button>
			</div>
		);
	}
}