import React from 'react';
import PropTypes from 'prop-types';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import CssBaseline from '@material-ui/core/CssBaseline';
import useScrollTrigger from '@material-ui/core/useScrollTrigger';
import Slide from '@material-ui/core/Slide';
import AddIcon from '@material-ui/icons/Add';
import { Box, Button, IconButton } from '@material-ui/core/';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import ForwardIcon from '@material-ui/icons/Forward';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';

const useStyles = makeStyles((theme = useTheme()) => ({
	typoClass:
	{
		display: "inline",
	},
	buttonpos:
	{
		display: "flex",
		justifyContent: "right"
	}

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
		<React.Fragment>
			<CssBaseline />
			<HideOnScroll {...props}>
				<AppBar>
					<Toolbar>
						{
							props.link !== undefined
								?
								<Box>
								
									<Typography className={classes.typoClass} variant="h6" >{props.title}</Typography>
									<IconButton aria-label="Добавить" href={props.link}>
										<AddIcon style={{ fontSize: 35, color: '#FFF' }} />
									</IconButton>
						

									{
										props.isLogIn
											?
											<Box >
												<Typography className={classes.typoClass} variant="h6" >{props.login}</Typography>
												<Button variant="contained" color="secondary" className={classes.button} startIcon={<ExitToAppIcon />}>
													Выйти
								  				</Button>
											</Box>
											:
											<Button variant="contained" className={classes.button} startIcon={<ForwardIcon />}>
												Войти
								  			</Button>
									}

								</Box>
								:
								<Typography variant="h6">{props.title}</Typography>
						}
					</Toolbar>
				</AppBar>
			</HideOnScroll>
			<Toolbar />
		</React.Fragment>
	);
}