import React, { Component } from 'react'
import { Grid, Typography, TextField, IconButton } from '@material-ui/core'
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import OrderItem from '../elements/OrderItem';
import AddIcon from '@material-ui/icons/Add';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';

class OrderItemList extends Component {

    constructor(props) {
        super(props);
        this.state = {
            items: null,
            loading: true,
            orderName: "",
            price: "",
            typeOfCargo_ID_FK: ""
        };

        this.SentItem = this.SentItem.bind(this);
        this.CreateItem = this.CreateItem.bind(this);
        this.DeleteItem = this.DeleteItem.bind(this);

        this.componentDidMount = this.componentDidMount.bind(this);

        this.onNameChange = this.onNameChange.bind(this);
        this.onPriceChange = this.onPriceChange.bind(this);
        this.onTypeOfCargo_ID_FKChange = this.onTypeOfCargo_ID_FKChange.bind(this);
    }


    onNameChange(e) { this.setState({ orderName: e.target.value }); }
    onPriceChange(e) { this.setState({ price: e.target.value }); }
    onTypeOfCargo_ID_FKChange(e) { this.setState({ typeOfCargo_ID_FK: e.target.value }); }


    componentDidMount() {
        this.setState({
            items: null,
            loading: true
        });
        const url = "http://localhost:5000/api/OrderItems/order/" + this.props.orderID;
        fetch(url, { method: 'GET' })
            .then(response => response.json())
            .then(result =>
                this.setState({
                    items: result,
                    loading: false
                }));
    }


    CreateItem(orderI) {
        const url = "http://localhost:5000/api/OrderItems/";
        var ordJSN = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                order_ID_FK: this.props.orderID,
                orderName: orderI.orderName,
                price: orderI.price,
                typeOfCargo_ID_FK: orderI.typeOfCargo_ID_FK
            })
        }
        fetch(url, ordJSN).then(this.props.updateOrder).then(this.componentDidMount);
    }

    DeleteItem(id) {
        fetch("http://localhost:5000/api/OrderItems/" + id, { method: 'DELETE' }).then(this.props.updateOrder).then(this.componentDidMount);;
    }

    SentItem(e) {
        e.preventDefault();
        var itemOrderName = this.state.orderName.trim();

        var itemPrice = parseInt(this.state.price);
        var itemTypeOfCargo_ID_FK = parseInt(this.state.typeOfCargo_ID_FK);

        if (!itemOrderName || itemPrice <= 0 || itemTypeOfCargo_ID_FK <= 0) {
            return;
        }
        this.CreateItem({ orderName: itemOrderName, price: itemPrice, typeOfCargo_ID_FK: itemTypeOfCargo_ID_FK });
    }

    render() {

        return (
            <Accordion style={ this.props.type==="past" ? {background: "#DDD"} :  this.props.status === 5 ? {background: "#EA0"}: {}}>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}>
                    <Typography>Список заказов</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    <List>

                        {
                            this.state.loading
                                ?
                                <Typography>Загрузка</Typography>
                                :
                                this.state.items.map((orderIt, index) => { return (<OrderItem itemContent={orderIt} key={index} deleteF={this.DeleteItem} type={this.props.type} role={this.props.role} status = {this.props.status}/>) })
                        }
                        {
                            (this.props.status === 1 || this.props.status === 5) && this.props.role !== "courier"
                                ?
                                <React.Fragment>
                                    <ListItem>
                                        <Grid item xs={12}>
                                            <TextField
                                                required
                                                label="Предмет"
                                                fullWidth
                                                onChange={this.onNameChange}
                                            />
                                        </Grid>
                                    </ListItem>
                                    <ListItem>
                                        <Grid item xs={12} sm={5}>
                                            <TextField
                                                required
                                                label="Ценность"
                                                fullWidth
                                                onChange={this.onPriceChange}
                                            />
                                        </Grid>

                                        <Grid item xs={12} sm={3}>
                                            <TextField
                                                required
                                                label="Тип"
                                                fullWidth
                                                onChange={this.onTypeOfCargo_ID_FKChange}
                                            />
                                        </Grid>

                                        <IconButton aria-label="Добавить" onClick={this.SentItem}>
                                            <AddIcon style={{ fontSize: 35, color: "#3B14AF" }} />
                                        </IconButton>
                                    </ListItem>
                                </React.Fragment>
                                :
                                <React.Fragment />
                        }

                    </List>

                </AccordionDetails>
            </Accordion>
        )
    }
}
export default (OrderItemList)

