import React, { Component } from 'react'
import { Box, Container, Grid, TextField, Button } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import ForwardIcon from '@material-ui/icons/Forward';

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
    },
    button: {
        marginTop: theme.spacing(2),
    },

});

class CustomerRegForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            confPassword: "",
            userName: ""
        };

        this.CreateCustomer = this.CreateCustomer.bind(this);
        this.SentCustomer = this.SentCustomer.bind(this);



        this.onEmailChange = this.onEmailChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);
        this.onUserNameChange = this.onUserNameChange.bind(this);
        this.onConfPasswordChange = this.onConfPasswordChange.bind(this);
    }


    onEmailChange(e) { this.setState({ email: e.target.value }); }
    onPasswordChange(e) { this.setState({ password: e.target.value }); }
    onUserNameChange(e) { this.setState({ userName: e.target.value }); }
    onConfPasswordChange(e) { this.setState({ confPassword: e.target.value }); }

    CreateCustomer(cust) {
        const url = "https://localhost:5001/api/orders/"; // don't know yet
        var ordJSN = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: cust.email,
                password: cust.password,
                confPassord: cust.confPassord,
                userName: cust.userName,
            })
        }
        fetch(url, ordJSN);
    }

    SentCustomer(e) {
        e.preventDefault();
        var custEmail = this.state.email.trim();
        var custPassword = this.state.password.trim();
        var custUserName = this.state.userName.trim();
        var custConfPassord = this.state.confPassord.trim();
        if (!custEmail || !custPassword || !custUserName || !custConfPassord) {
            return;
        }
        this.CreateOrder({ email: custEmail, password: custPassword, confPassord: custConfPassord, userName: custUserName });
        document.location.href = "/";
    }

    render() {
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />

                <Navbar title={"Регистрация"} />

                <Box className={this.props.classes.paper}>
                    <Grid container spacing={1}>
                        <Grid>


                                    <Card>
                                        <CardContent>

                                            <Grid container spacing={3}>

                                            <Grid item xs={12}>
                                                    <TextField
                                                        id="userName"
                                                        required
                                                        label="ФИО"
                                                        fullWidth
                                                        onChange={this.onUserNameChange}
                                                    />
                                                </Grid>

                                                <Grid item xs={12}>
                                                    <TextField
                                                        id="email"
                                                        required
                                                        label="e-mail"
                                                        fullWidth
                                                        onChange={this.onEmailChange}
                                                    />
                                                </Grid>

                                                <Grid item xs={12}>
                                                    <TextField
                                                        id="password"
                                                        required
                                                        label="Пароль"
                                                        fullWidth
                                                        onChange={this.onPasswordChange}
                                                    />
                                                </Grid>

                                                <Grid item xs={12}>
                                                    <TextField
                                                        id="confPassword"
                                                        required
                                                        label="Подтвердите пароль"
                                                        fullWidth
                                                        onChange={this.onConfPasswordChange}
                                                    />
                                                </Grid>

                                                </Grid>
                                            <Button
                                                variant="contained"
                                                color="primary"
                                                className={this.props.classes.button}
                                                startIcon={<ForwardIcon />}
                                                onClick={this.SentOrder}
                                            >
                                                Зарегестрироваться
                                        </Button>

                                        </CardContent>
                                    </Card>

                        </Grid>
                    </Grid>
                </Box>
            </Container >
        )
    }
}
export default withStyles(styles)(CustomerRegForm)

