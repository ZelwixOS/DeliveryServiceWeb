import React from 'react';
import PropTypes from 'prop-types';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import CssBaseline from '@material-ui/core/CssBaseline';
import useScrollTrigger from '@material-ui/core/useScrollTrigger';
import Slide from '@material-ui/core/Slide';
import AddIcon from '@material-ui/icons/Add';
import { IconButton } from '@material-ui/core/';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import LogInForm from '../elements/LogInForm';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles((theme = useTheme()) => ({
	typoClass:
	{
		display: "inline",
		padding: theme.spacing(1)
	},
	buttonpos:
	{
		display: "flex",
		justifyContent: "right"
	},
	root: {
		flexGrow: 1,
		'& > *': {
		  margin: theme.spacing(1),
		},
	  },

}));


function HideOnScroll(props) {
	const { children, window } = props;
	const trigger = useScrollTrigger({ target: window ? window() : undefined });

	return (
		<Slide appear={false} direction="down" in={!trigger}>
			{children}
		</Slide>
	);
}


HideOnScroll.propTypes = {
	children: PropTypes.element.isRequired,
};

export default function Navbar(props) {
	const classes = useStyles();


	return (
		<React.Fragment >
			<CssBaseline />
			<HideOnScroll {...props} >
				<AppBar >
					<Toolbar>
					<div style={{margin: "10px"}}>
						<Typography variant="h6" >{props.title}</Typography>
					</div>
						{
							props.link !== undefined && props.role === "customer"
								&&
								
									<IconButton aria-label="Добавить" href={props.link}>
										<AddIcon style={{ fontSize: 35, color: '#FFF' }} />
									</IconButton>
						}
						 <LogInForm /> 
						
						{
							props.role === "admin" &&
							<React.Fragment>
								 <div className={classes.root}>
								<Button   variant="contained" href="/orders">
									Заказы
								 </Button>
								<Button variant="contained" href="/types">
									Типы
								</Button>
								<Button variant="contained" href="/couriers">
									Курьеры
								</Button>
								<Button variant="contained" href="/courierForm">
									Регистрация курьеров
								</Button>
								</div>
							</React.Fragment>
						}
					</Toolbar>
				</AppBar>
			</HideOnScroll>
			<Toolbar />
		</React.Fragment>
	);
}