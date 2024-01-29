import { useEffect, useState } from "react";
import { AppBar, Avatar, Box, Container, IconButton, Menu, MenuItem, Toolbar, Tooltip, Typography } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import Brightness4Icon from "@mui/icons-material/Brightness4";
import logo from "../assets/logo.png";
import config from "../config/config.json";
import { NavLink, useLocation } from "react-router-dom";
import SubHeader from "./SubHeader";
import EastSharpIcon from '@mui/icons-material/EastSharp';

const NavBar = () => {
    const pagesData = config["EN"];
    const location = useLocation();
    const iconStyle = { fontSize: 16, marginLeft: "5px" };
    const loggedIn = false;

    useEffect(() => {
        console.log(location.pathname);
    }, [location]);

    const [anchorElNav, setAnchorElNav] = useState<null | HTMLElement>(
        null
    );
    const [anchorElUser, setAnchorElUser] = useState<null | HTMLElement>(
        null
    );

    const pages = pagesData.pages.map(page => ({ ...page }));
    const settings = pagesData.settings || [];

    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const [scrolled, setScrolled] = useState(false);
    useEffect(() => {
        const handleScroll = () => {
            const offset = window.scrollY;
            if (offset > 130) {
                setScrolled(true);
            } else {
                setScrolled(false);
            }
        };

        window.addEventListener("scroll", handleScroll);

        return () => {
            window.removeEventListener("scroll", handleScroll);
        };
    }, []);

    return (
        <>
            <SubHeader />
            <AppBar
                position="sticky"
                sx={{
                    backgroundColor: '#fff',
                    textColor: "#000",
                    boxShadow: "none",
                    borderBottom: scrolled ? "1px solid #ddd" : "none",
                }}
                className="mt-0"
            >
                <Container maxWidth="xl">
                    <Toolbar disableGutters>
                        <img
                            height={40}
                            width={40}
                            src={logo}
                            alt=""
                            className="mr-2"
                        />
                        <Box
                            sx={{
                                flexGrow: 1,
                                display: { xs: "flex", md: "none" },
                            }}
                        >
                            <IconButton
                                size="large"
                                aria-label="account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleOpenNavMenu}
                                color="inherit"
                            >
                                <MenuIcon className="text-gray-500" />
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorElNav}
                                anchorOrigin={{
                                    vertical: "bottom",
                                    horizontal: "left",
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: "top",
                                    horizontal: "left",
                                }}
                                open={Boolean(anchorElNav)}
                                onClose={handleCloseNavMenu}
                                sx={{
                                    display: {
                                        xs: "block",
                                        md: "none",
                                    },
                                }}
                            >
                                {pages.map((page) => (
                                    <MenuItem
                                        key={page.url}
                                        onClick={handleCloseNavMenu}
                                    >
                                        <Typography textAlign="center">
                                            {page.name}
                                        </Typography>
                                    </MenuItem>
                                ))}
                            </Menu>
                        </Box>
                        <Box
                            sx={{
                                flexGrow: 1,
                                display: { xs: "none", md: "flex" },
                            }}
                        >
                            {pages.map((page) => (
                                <NavLink
                                    key={page.url}
                                    to={page.url}
                                    className={`text-black m-3 inline-block transition duration-300 border-b-2 border-transparent hover:border-blue-500 ${page.url === location.pathname ? "border-blue-300" : ""
                                        }`}
                                    onClick={handleCloseNavMenu}
                                >
                                    <span
                                        className={`my-2 ${page.url === location.pathname ? "text-blue-500" : "text-black"
                                            }`}
                                    >
                                        {page.name}
                                    </span>
                                </NavLink>
                            ))}
                        </Box>
                        <Brightness4Icon className="mr-5" />
                        {!loggedIn ? (<button type="button" className="text-white bg-blue-700 hover:bg-blue-800  font-medium rounded-md text-sm px-3 py-1.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">
                            <a href="/sign-in">
                                Sign In
                                <EastSharpIcon style={iconStyle} />
                            </a>
                        </button>) : (<Box sx={{ flexGrow: 0 }}>
                            <Tooltip title="Open settings">
                                <IconButton
                                    onClick={handleOpenUserMenu}
                                    sx={{ p: 0 }}
                                >
                                    <Avatar
                                        alt="Remy Sharp"
                                        src="/static/images/avatar/2.jpg"
                                    />
                                </IconButton>
                            </Tooltip>
                            <Menu
                                sx={{ mt: "45px" }}
                                id="menu-appbar"
                                anchorEl={anchorElUser}
                                anchorOrigin={{
                                    vertical: "top",
                                    horizontal: "right",
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: "top",
                                    horizontal: "right",
                                }}
                                open={Boolean(anchorElUser)}
                                onClose={handleCloseUserMenu}
                            >
                                {settings.map((setting) => (
                                    <MenuItem
                                        key={setting}
                                        onClick={handleCloseUserMenu}
                                    >
                                        <Typography textAlign="center">
                                            {setting}
                                        </Typography>
                                    </MenuItem>
                                ))}
                            </Menu>
                        </Box>)}

                    </Toolbar>
                </Container>
            </AppBar>
        </>
    )
}

export default NavBar;
