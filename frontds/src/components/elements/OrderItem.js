import React, { Component } from 'react';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import { IconButton } from '@material-ui/core'

class OrderItem extends Component {

  constructor(props) {
    super(props);
    this.state = {
      deleted: false
    }
    this.onClick = this.onClick.bind(this);
  }

  onClick(e) {
    this.props.deleteF(this.props.itemContent.id);
    this.setState({
      deleted : true
  })

  }

  render() {
    var orderIt = this.props.itemContent;
    return (

      <React.Fragment>
        { this.state.deleted
          ?
          <React.Fragment>
          </React.Fragment>
          :
          <ListItem >
            <ListItemText primary={"Имя: " + orderIt.orderName + "; Ценность:" + orderIt.price + "; Тип: " + orderIt.typeOfCargoS} />
            {
              this.props.status === 1 && this.props.role!=="courier" &&  
            <IconButton aria-label="Удалить" onClick={this.onClick}>
            <HighlightOffIcon style={{ fontSize: 35, color: "#3B14AF" }} />
            </IconButton>
            }


          </ListItem>
        }
        <Divider />
      </React.Fragment>
    );
  }
}
export default (OrderItem)