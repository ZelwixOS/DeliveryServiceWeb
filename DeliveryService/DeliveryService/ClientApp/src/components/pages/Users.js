import React, { Component } from 'react'
import { Box, Container, Grid, Typography } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import axios from 'axios';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';


const styles = (theme) => ({
    paper: {
        marginTop: theme.spacing(2),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    cards:
    {
        margin: '5px',
        padding: '5px'
    }
});

axios.defaults.withCredentials = true
const comUrl = "http://localhost:5000"

class Users extends Component {
    constructor(props) {
        super(props);
        this.state = {
            users: null,
            role: "",
            loading: true
        };

        this.ComponentDidMount = this.componentDidMount.bind(this);
    }


    componentDidMount() {
        var url1 = comUrl + "/api/Account/Role/";
        axios.post(url1).then((response) => this.setState({ role: response.data }));

        var url = comUrl + "/api/Users";
        axios.get(url).then((response) => this.setState({ users: response.data, loading: false }));
    }

    render() {
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Navbar title={"Типы грузов"} link={"/courierForm/"} role={this.state.role} />

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

                                        
                                        <Typography variant="h5" gutterBottom>
                                        Курьеры
                                          </Typography>
                                       
                                            {
                                                this.state.users.couriers.map((user, index) => {
                                                    return (
                                                        <Card className={styles.cards} key={index}>
                                                             
                                                            <Typography variant="h6" gutterBottom>
                                                                Пользователь {user.userName}
                                                            </Typography>
                                                            <List>
                                                            <ListItem >
                                                                <ListItemText primary={"Имя: " + user.secondName + " " + user.firstName} />
                                                            </ListItem>
                                                            <ListItem >
                                                                <ListItemText primary={"email: " + user.email} />
                                                            </ListItem>
                                                            <ListItem >
                                                                <ListItemText primary={"Телефон: " + user.phoneNumber} />
                                                            </ListItem>
                                                            </List>
                                                        </Card>

                                                    )
                                                })
                                            }

                                        <Typography variant="h5" gutterBottom>
                                          Пользователи
                                          </Typography>
                                            {
                                                this.state.users.customers.map((user, index) => {
                                                    return (
                                                        <Card className={styles.cards} key={index}>
                                                            <Typography variant="h6" gutterBottom>
                                                                Пользователь {user.userName}
                                                            </Typography>
                                                            <List>
                                                            <ListItem >
                                                                <ListItemText primary={"Имя: " + user.secondName + " " + user.firstName} />
                                                            </ListItem>
                                                            <ListItem >
                                                                <ListItemText primary={"email: " + user.email} />
                                                            </ListItem>
                                                        </List>
                                                        </Card>
                                                    )
                                                })
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
export default withStyles(styles)(Users)

