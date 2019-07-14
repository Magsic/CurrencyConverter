import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Converter } from './components/Converter';
import { Visualisation } from './components/Visualisation';

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<Layout>
				<Route path='/converter' component={Converter} />
				<Route path='/visualisation' component={Visualisation} />
			</Layout>
		);
	}
}
