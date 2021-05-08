import React, { Component } from 'react';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import { IconButton } from '@material-ui/core'
import DoneIcon from '@material-ui/icons/Done';
import axios from 'axios';

const comUrl = "http://localhost:5000"

class CargoTypeItem extends Component {

    constructor(props) {
        super(props);
        this.state = {
            active: props.type.active
        }

        this.onClickOff = this.onClickOff.bind(this);
        this.onClickOn = this.onClickOn.bind(this);
    }

    onClickOff() {
        var url = comUrl + "/api/TypesOfCargo/off/" + this.props.type.id;
        axios.post(url).then((response) => this.changeMode(response.data));
    }

    onClickOn() {
        var url = comUrl + "/api/TypesOfCargo/on/" + this.props.type.id;
        axios.post(url).then((response) => this.changeMode(response.data));
    }

    changeMode(resp)
    {
        if (resp === "")
        {
            var newActive = !this.state.active;
            this.setState({
                active: newActive
            })
        }
    }

    render() {
        return (

            <React.Fragment>
                <ListItem key={this.props.key}>
                    <ListItemText primary={this.props.type.typeName + " - " + this.props.type.coefficient} />
                    {
                        this.state.active === true
                        ?
                        <IconButton aria-label="Отключить" onClick={this.onClickOff}>
                            <HighlightOffIcon style={{ fontSize: 35, color: "#03a9f4" }} />
                        </IconButton>
                        :
                        <IconButton aria-label="Включить" onClick={this.onClickOn}>
                            <DoneIcon style={{ fontSize: 35, color: "#03a9f4" }} />
                        </IconButton>
                    }
                </ListItem>
                <Divider />
            </React.Fragment>
        );
    }
}
export default (CargoTypeItem)