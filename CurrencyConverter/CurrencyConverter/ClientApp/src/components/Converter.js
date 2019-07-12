import React, { Component } from 'react';
import DatePicker from 'react-datepicker';
//import Dropdown from 'react-dropdown-select';

import "react-datepicker/dist/react-datepicker.css";

export class Converter extends Component {
	static displayName = Converter.name;

	constructor(props) {
		super(props);
		this.state = {
			currentCount: 0,
			date: new Date(),
			//originCurrency: "---",
			//destinationCurrency: "HRK"
		};
		this.incrementCounter = this.incrementCounter.bind(this);
		this.handleDateChange = this.handleDateChange.bind(this);
		//this.setOriginCurrency = this.setOriginCurrency.bind(this);
		//this.setDestinationCurrency = this.setDestinationCurrency.bind(this);
	}

	incrementCounter() {
		this.setState({
			currentCount: this.state.currentCount + 1,
			date: this.state.date,
			//originCurrency: this.state.originCurrency,
			//destinationCurrency: this.state.destinationCurrency
		});
	}

	handleDateChange(date) {
		this.setState({
			currentCount: this.state.currentCount,
			date: date,
			//originCurrency: this.state.originCurrency,
			//destinationCurrency: this.state.destinationCurrency
		})
	}

	//setOriginCurrency(currency) {
	//	this.setState({
	//		currentCount: this.state.currentCount,
	//		date: this.state.date,
	//		originCurrency: currency[0],
	//		destinationCurrency: this.state.destinationCurrency
	//	})
	//}

	//setDestinationCurrency(currency) {
	//	this.setState({
	//		currentCount: this.state.currentCount,
	//		date: this.state.date,
	//		originCurrency: this.state.originCurrency,
	//		destinationCurrency: currency[0]
	//	})
	//}

	render() {
		return (
			<div>
				<h1>Converter</h1>

				<p>Current count: <strong>{this.state.currentCount}</strong></p>

				<DatePicker
					maxDate={new Date()}
					selected={this.state.date}
					onChange={this.handleDateChange}
				/>

				<input type="text"></input>
				<input disabled type="text"></input>

				<button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
			</div>
		);
	}
}
//<Dropdown options={["---", "HRK", "AUD", "EUR"]} onChange={(currency) => this.setOriginCurrency(currency)} />

//<Dropdown options={["HRK", "AUD", "EUR"]} onChange={(currency) => this.setDestinationCurrency(currency)} />
