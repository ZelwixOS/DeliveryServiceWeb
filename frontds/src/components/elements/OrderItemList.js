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
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import axios from 'axios';
import FormControl from '@material-ui/core/FormControl';

axios.defaults.withCredentials = true
const comUrl = "http://localhost:5000"

const styles = (theme) => ({
    formControl: {
        margin: theme.spacing(1),
        minWidth: 120,
      }
});


class OrderItemList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: null,
            loading1: true,
            loading2: true,
            types: null,
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
            loading1: true,
            types: null,
            loading2: true
        });
        const url = comUrl+ "/api/OrderItems/order/" + this.props.orderID;
        axios.get(url).then((response) =>  this.setState({
            items: response.data, loading1: false}));
           
        const url1 = comUrl+ "/api/TypesOfCargo/";
        axios.get(url1).then((response) =>  this.setState({
            types: response.data,
            loading2: false}));
    }


    CreateItem(orderI) {
        const url = comUrl + "/api/OrderItems/";
        var value = { 
            "order_ID_FK": this.props.orderID,
            "orderName": orderI.orderName,
            "price": orderI.price,
            "typeOfCargo_ID_FK": orderI.typeOfCargo_ID_FK
            };
           axios.post(url, value).then(this.props.updateOrder).then(this.componentDidMount);
    }

    DeleteItem(id) {
        const url = comUrl + "/api/OrderItems/"+ id;
           axios.delete(url).then(this.props.updateOrder).then(this.componentDidMount);
    } 
//  переделай!
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
            <Accordion style={this.props.type === "past" ? { background: "#DDD" } : this.props.status === 5 ? { background: "#EA0" } : {}}>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}>
                    <Typography>Список заказов</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    <List>

                        {
                            this.state.loading1||this.state.loading2
                                ?
                                <Typography>Загрузка</Typography>
                                :
                                this.state.items.map((orderIt, index) => { return (<OrderItem itemContent={orderIt} key={index} deleteF={this.DeleteItem} type={this.props.type} role={this.props.role} status={this.props.status} />) })
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
                                        <Grid item xs={12} sm={6}>
                                            <TextField
                                                required
                                                label="Ценность"
                                                fullWidth
                                                onChange={this.onPriceChange}
                                            />
                                        </Grid>
                                        </ListItem>
                                        <ListItem>

                                        <FormControl className={styles.formControl}>
                                        <InputLabel id="TypeLabel">Тип</InputLabel>
                                        <Select
                                            required
                                            labelId="TypeLabel"
                                            id="Type"
                                            value={this.state.typeOfCargo_ID_FK}
                                            onChange={this.onTypeOfCargo_ID_FKChange}>
                                            {
                                                this.state.types !== null
                                                &&
                                                this.state.types.map((type, index) => { return (<MenuItem key={index} value={type.id}>{type.typeName}</MenuItem>) })

                                            }
                                        </Select>
                                        </FormControl>

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

