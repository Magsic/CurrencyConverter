import React, { Component } from 'react';
import DatePicker from 'react-datepicker';
//import Dropdown from 'react-dropdown-select';

import "react-datepicker/dist/react-datepicker.css";

export class Converter extends Component {
	static displayName = Converter.name;

	constructor(props) {
		super(props);
		this.state = {
			date: new Date(),
			originCurrency: "---",
			destinationCurrency: "HRK",
			allCurrencies: ["HRK", "AUD", "EUR"]
		};
		this.handleDateChange = this.handleDateChange.bind(this);
		this.setOriginCurrency = this.setOriginCurrency.bind(this);
		this.setDestinationCurrency = this.setDestinationCurrency.bind(this);
		this.doConversion = this.doConversion.bind(this);
	}

	componentDidMount() {
		fetch('/Rates/GetCurrencies')
			.then(response => response.json())
			.then(data => this.setState({
				date: this.state.date,
				originCurrency: this.state.originCurrency,
				destinationCurrency: this.state.destinationCurrency,
				allCurrencies: data
			}));
	}

	handleDateChange(date) {
		this.setState({
			date: date,
			originCurrency: this.state.originCurrency,
			destinationCurrency: this.state.destinationCurrency
		})
	}

	setOriginCurrency(event) {
		this.setState({
			date: this.state.date,
			originCurrency: event.target.value,
			destinationCurrency: this.state.destinationCurrency
		})
	}

	setDestinationCurrency(event) {
		this.setState({
			date: this.state.date,
			originCurrency: this.state.originCurrency,
			destinationCurrency: event.target.value
		})
	}

	doConversion() {

	}

	render() {
		var options = ["---", ...this.state.allCurrencies];
		var originOptions = options.map(c => <option key={c} value={c}>{c}</option>);
		var destinationOptions = this.state.allCurrencies.map(c => <option key={c} value={c}>{c}</option>);

		return (
			<div>
				<h1>Converter</h1>
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
					<input defaultValue="1" step="0.1" min="0" type="number"></input>

					<select value={this.state.originCurrency} onChange={this.setOriginCurrency} >
						{originOptions}
					</select>
				</div>
				<br />

				<div>
					<input disabled type="text"></input>

					<select value={this.state.destinationCurrency} onChange={this.setDestinationCurrency} >
						{destinationOptions}
					</select>
				</div>
				<br/>
				<button onClick={this.doConversion}>Converte</button>
			</div>
		);
	}
}