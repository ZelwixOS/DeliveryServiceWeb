import React, { Component } from 'react'
import { Box, Container, Grid, Typography, TextField, IconButton } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import axios from 'axios';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import AddIcon from '@material-ui/icons/Add';

const styles = (theme) => ({
    paper: {
        marginTop: theme.spacing(2),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    }
});

axios.defaults.withCredentials = true
const comUrl = "http://localhost:5000"

class Types extends Component {
    constructor(props) {
        super(props);
        this.state = {
            types: null,
            typeName: "",
            coefficient: "",
            role:"",
            loading: true
        };

        this.ComponentDidMount = this.componentDidMount.bind(this);
        this.SentItem = this.SentItem.bind(this);
        this.CreateItem = this.CreateItem.bind(this);

        this.onNameChange = this.onNameChange.bind(this);
        this.onCoefChange = this.onCoefChange.bind(this);
    }

    onNameChange(e) { this.setState({ typeName: e.target.value }); }
    onCoefChange(e) { this.setState({ coefficient: e.target.value }); }

    CreateItem(type) {
        const url = comUrl + "/api/TypesOfCargo/";
        var ordJSN = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                typeName: type.typeName,
                coefficient: type.coefficient,
            })
        }

        fetch(url, ordJSN).then(this.ComponentDidMount);
    }

    SentItem(e) {
        e.preventDefault();
        var itemTypeName = this.state.typeName.trim();
        var itemCoef = parseInt(this.state.coefficient);
        if (!itemTypeName || itemCoef <= 0) {
            return;
        }
        this.CreateItem({ typeName: itemTypeName, coefficient: itemCoef });
    }


    componentDidMount() {
        var url1 = comUrl + "/api/Account/Role/";
        axios.post(
            url1, { withCredentials: true }
        ).then((response) => this.setState({ role: response.data }));

        var url = comUrl + "/api/TypesOfCargo";
        axios.get(
            url, { withCredentials: true }
        ).then((response) => this.setState({ types: response.data, loading: false }));
    }

    render() {
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Navbar title={"Типы грузов"} role={this.state.role} />
                <Box className={this.props.classes.paper}>
                    <Grid container spacing={1}>

                        {
                            this.state.loading
                                ?
                                <Typography className={this.props.classes.loading}>
                                    Загрузка...
                                    </Typography >
                                :
                                <React.Fragment>
                                    <Card>
                                    <List>
                                            <ListItem>
                                                <Grid item xs={12}>
                                                    <TextField
                                                        required
                                                        label="Имя типа"
                                                        fullWidth
                                                        onChange={this.onNameChange}
                                                    />
                                                </Grid>
                                            </ListItem>
                                            <ListItem>
                                                <Grid item xs={12} sm={5}>
                                                    <TextField
                                                        required
                                                        label="Процент"
                                                        fullWidth
                                                        onChange={this.onCoefChange}
                                                    />
                                                </Grid>
                                                <IconButton aria-label="Добавить" onClick={this.SentItem}>
                                                    <AddIcon style={{ fontSize: 35, color: "#3B14AF" }} />
                                                </IconButton>

                                            </ListItem>

                                        </List>
                                        <List>
                                            {
                                                this.state.types.map((type, index) => {
                                                    return (<ListItem >
                                                        <ListItemText primary={type.typeName + " - " + type.coefficient} key={index} />
                                                    </ListItem>)
                                                })
                                            }
                                        </List>
                                    </Card>
                                </React.Fragment>
                        }
                    </Grid>
                </Box>
            </Container >
        )
    }
}
export default withStyles(styles)(Types)

