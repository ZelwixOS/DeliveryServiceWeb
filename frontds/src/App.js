import React, { Component } from 'react';

import {
  Route,
  Switch,
  withRouter
} from "react-router-dom"

import './App.css';

import OrdersRedir from '../src/components/redirection/OrdersRedir';
import OrderFormRedir from '../src/components/redirection/OrderFormRedir';
import OrderForm from '../src/components/pages/OrderForm';
import CustomerRegFormRedir from '../src/components/redirection/CustomerRegFormRedir';

class App extends Component {
  render() {
    const { history } = this.props

    return (
      <div className="App">

        <Switch>
          <Route history={history} path='/orders' component={OrdersRedir} />
          <Route exact history={history} path='/' component={OrdersRedir} />
          <Route history={history} path='/orderForm/:slug' component={OrderFormRedir} />
          <Route history={history} path='/orderForm' component={OrderForm} />
          <Route  history={history} path='/registration' component={CustomerRegFormRedir} />
        </Switch>
      </div>
    );
  }
}

export default withRouter(App)
