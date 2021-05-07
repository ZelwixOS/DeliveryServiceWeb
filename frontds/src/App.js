import React, { Component } from 'react';
import { createMuiTheme } from '@material-ui/core/styles';
import { ThemeProvider } from '@material-ui/styles';

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
import CourierRegFormRedir from '../src/components/redirection/CourierRegFormRedir';
import TypesRedir from '../src/components/redirection/TypesRedir';
import CouriersRedir from '../src/components/redirection/CouriersRedir';



class App extends Component {
  render() {
    const { history } = this.props

    const theme = createMuiTheme({
      palette: {
        primary: {
          main: '#2196f3',
        },
        secondary: {
          main: '#ffeb3b',
        },
      },
    });

    return (
  <ThemeProvider theme={theme}>
      <div className="App">

        <Switch>
          <Route history={history} path='/orders' component={OrdersRedir} />
          <Route exact history={history} path='/' component={OrdersRedir} />
          <Route history={history} path='/orderForm/:slug' component={OrderFormRedir} />
          <Route history={history} path='/orderForm' component={OrderForm} />
          <Route  history={history} path='/registration' component={CustomerRegFormRedir} />
          <Route  history={history} path='/types' component={TypesRedir} />
          <Route  history={history} path='/courierForm' component={CourierRegFormRedir} />
          <Route  history={history} path='/couriers' component={CouriersRedir} />
        </Switch>
      </div>
      </ThemeProvider>
    );
  }
}

export default withRouter(App)
