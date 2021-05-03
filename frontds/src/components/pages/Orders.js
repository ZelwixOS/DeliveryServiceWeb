import React, { Component } from 'react'
import { Box, Container, Grid, Typography } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import OrderCard from '../elements/OrderCard'
import axios from 'axios';

const styles = (theme) => ({
    btnMenu: {
        width: '100%',
        textAlign: 'left',
    },
    paper: {
        marginTop: theme.spacing(2),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    text: {
        fontSize: '16px',
        padding: theme.spacing(1),
    }
});


axios.defaults.withCredentials = true
const comUrl = "http://localhost:5000"

class Orders extends Component {
    constructor(props) {
        super(props);
        this.state = {
            active: null,
            available: null,
            past: null,
            role: null,
            loading: true
        };
        this.ComponentDidMount = this.componentDidMount.bind(this);
    }


    componentDidMount() {
        var url = comUrl + "/api/orders";
        axios.get(
            url, { withCredentials: true }
        )
            .then((response) =>
                this.setState({ active: response.data.active, available: response.data.available, past: response.data.past, role: response.data.role, loading: false }));
    }

    render() {
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Navbar title={"Заказы"} link={"/orderForm/"} role={this.state.role}>  </Navbar>

                <Box className={this.props.classes.paper}>
                    <Grid container spacing={1}>
                        <Grid>
                            {
                                this.state.loading
                                    ?
                                    <Typography className={this.props.classes.loading}>
                                        Загрузка...
                                    </Typography >
                                    :
                                    <React.Fragment>
                                        {
                                            this.state.active.length !== 0
                                                ?
                                                <React.Fragment>
                                                    <Typography variant="h4" >
                                                        Активные заказы
                                                    </Typography >
                                                    {
                                                        this.state.active.map((order, index) => { return (<OrderCard orderContent={order} key={index} type="active" role = {this.state.role} listUpdate={this.ComponentDidMount}/>) })
                                                    }

                                                </React.Fragment>
                                                :
                                                <React.Fragment />
                                        }
                                        {
                                            this.state.available.length !== 0
                                                ?
                                                <React.Fragment>
                                                    <Typography variant="h4" >
                                                        Доступные заказы
                                                    </Typography >
                                                    {
                                                        this.state.available.map((order, index) => { return (<OrderCard orderContent={order} key={index} type="available" role = {this.state.role} listUpdate={this.ComponentDidMount}/>) })
                                                    }

                                                </React.Fragment>
                                                :
                                                <React.Fragment />
                                        }
                                        {
                                            this.state.past.length !== 0
                                                ?
                                                <React.Fragment>
                                                    <Typography variant="h4" >
                                                        Прошлые заказы
                                                    </Typography >
                                                    {
                                                        this.state.past.map((order, index) => { return (<OrderCard orderContent={order} key={index} type="past" role = {this.state.role} listUpdate={this.ComponentDidMount}/>) })
                                                    }

                                                </React.Fragment>
                                                :
                                                <React.Fragment />
                                        }
                                    </React.Fragment>

                            }
                        </Grid>
                    </Grid>
                </Box>
            </Container >
        )
    }
}
export default withStyles(styles)(Orders)
