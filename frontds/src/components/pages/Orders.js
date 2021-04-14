import React, { Component } from 'react'
import { Box, Container, Grid, Typography } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import CourseCard from '../elements/OrderCard'

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

class Orders extends Component {
    constructor(props) {
        super(props);
        this.state = {
            orders: null,
            loading: true
        };
    }


    componentDidMount() {
        fetch("http://localhost:5000/api/orders")
            .then(response => response.json())
            .then(result =>
                this.setState({ orders: result, loading: false }));
    }

    render() {
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Navbar title={"Заказы"} link={"/orderForm/"}>  </Navbar>
    
                <Box className={this.props.classes.paper}>
                    <Grid container spacing={1}>
                        <Grid>
                            {
                                this.state.loading
                                    ? <Typography className={this.props.classes.loading}>
                                        Загрузка...
                                    </Typography >
                                    : 
                                        this.state.orders.map((order, index) =>  {return (<CourseCard orderContent={order} key={index}/>)})
                            }
                        </Grid>
                    </Grid>
                </Box>
            </Container >
        )
    }
}
export default withStyles(styles)(Orders)
