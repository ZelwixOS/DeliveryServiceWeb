import React, { Component } from 'react'
import { Grid, TextField } from '@material-ui/core'
import NavigateNextRoundedIcon from '@material-ui/icons/NavigateNextRounded';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';

class LogInForm extends Component {

    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: ""
        };

        this.SentItem = this.SentItem.bind(this);
        this.Authorize = this.Authorize.bind(this);


        this.componentDidMount = this.componentDidMount.bind(this);

        this.onEmailChange = this.onEmailChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);

    }


    onEmailChange(e) { this.setState({ email: e.target.value }); }
    onPasswordChange(e) { this.setState({ password: e.target.value }); }


    Authorize(userInfo) {
        const url = "https://localhost:5001/api/OrderItems/";// no idea yet
        var ordJSN = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: userInfo.orderName,
                password: userInfo.password
            })
        }
        fetch(url, ordJSN);
    }

    SentItem(e) {
        e.preventDefault();
        var userEmail = this.state.email.trim();
        var userPassword = this.state.email.trim();


        if (!userEmail || !userPassword) {
            return;
        }
        this.Authorize({ email: userEmail, password: userPassword });
    }

    render() {

        return (
            <React.Component>

                <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">   {/* передай handleClose и open */}
                    <DialogTitle id="form-dialog-title">Вход</DialogTitle>
                    <DialogContent>

                        <Grid item xs={12}>
                            <TextField
                                required
                                label="Адрес эл. почты"
                                fullWidth
                                onChange={this.onEmailChange}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <TextField
                                required
                                label="Пароль"
                                fullWidth
                                onChange={this.onPasswordChange}
                            />
                        </Grid>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} color="primary">
                            Закрыть
                        </Button>
                        <Button onClick={this.SentItem} color="primary">
                            <NavigateNextRoundedIcon style={{ fontSize: 35, color: "#3B14AF" }} /> Войти
                        </Button>
                    </DialogActions>
                </Dialog>

                <Link href="#">Ещё не зарегистрированы?</Link>
            </React.Component>
        )
    }
}
export default (LogInForm)

