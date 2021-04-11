import React from 'react';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import { Button, Typography } from '@material-ui/core';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import OrderItemList from "../elements/OrderItemList";

const useStyles = makeStyles((theme = useTheme()) => ({
  root: {

    //width: '60%',
    marginBottom: theme.spacing(2)
  },

}));


export default function CourseCard(props) {
  const classes = useStyles();
  const orderContent = props.orderContent;

  return (
    <Card className={classes.root} variant="outlined" >
      <CardContent>
        <Typography className={classes.title} gutterBottom>
          Заказ {orderContent.id}
        </Typography>
        <List component="nav" className={classes.root} aria-label="Заказ">
          <ListItem >
            
              <ListItemText primary={"Стоимость: "+orderContent.cost} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Начальный адрес: "+orderContent.adressOrigin} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Дата заказа: "+orderContent.orderDateS} />
          </ListItem>
          <ListItem >
            
              <ListItemText primary={"Адрес доставки: "+orderContent.adressDestination} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Крайний срок: "+orderContent.deadlineS} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Имя получателя: "+orderContent.receiverName} />
          </ListItem>
          <Divider />
{
  orderContent.addNote!=null
  ?
  <React.Component>
     <ListItem >
          <ListItemText primary={"Примечание: "+orderContent.addNote} />
      </ListItem>
      <Divider />
  </React.Component>
      :

      <React.Component/>
}

{
  orderContent.delivery_ID_FK!=null
  ?
  <React.Component>
          <ListItem >
            <ListItemText primary={"Код доставки: "+orderContent.delivery_ID_FK} />
        </ListItem>
        <Divider />
  </React.Component>
      :

      <React.Component/>
}


          <ListItem >
              <ListItemText primary={"Код получателя:"+orderContent.customer_ID_FK + ". " + orderContent.customerS} />
          </ListItem>
          <Divider />
          <ListItem >

              <ListItemText primary={"Статус: "+orderContent.status.statusName} />
          </ListItem>
          <Divider />
          <ListItem >

              <ListItemText primary={"Код курьера: "+orderContent.courier_ID_FK + ". " + orderContent.courierS} />
          </ListItem>
          <Divider />

          <OrderItemList orderID={orderContent.id}/>



        </List>
      </CardContent>
      <CardActions>
        <Button size="small" href={"/orderForm/" + orderContent.id}> Редактировать </Button>
        <Button size="small" onClick={() => {
          fetch("https://localhost:5001/api/orders/" + orderContent.id, { method: 'DELETE' }).then(response => response.json());
          document.location.href = "/";
        }}>
          Удалить </Button>
      </CardActions>
    </Card>
  );
}