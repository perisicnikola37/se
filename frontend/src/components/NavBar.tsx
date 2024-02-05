import { useEffect, useState } from "react";
import {
    AppBar,
    Avatar,
    Box,
    Container,
    IconButton,
    Menu,
    MenuItem,
    Toolbar,
    Tooltip,
    Typography,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import Brightness4Icon from "@mui/icons-material/Brightness4";
import logo from "../assets/logo.png";
import config from "../config/config.json";
import { Link, NavLink, useLocation, useNavigate } from "react-router-dom";
import SubHeader from "./SubHeader";
import EastSharpIcon from "@mui/icons-material/EastSharp";
import { useUser } from "../contexts/UserContext";
import { useModal } from "../contexts/GlobalContext";
import userPicture from "../../src/assets/profile_image.jpg";
import { useDarkMode } from "../contexts/DarkModeContext";
import NightlightIcon from '@mui/icons-material/Nightlight';

const NavBar = () => {
    const navigate = useNavigate();
    const pagesData = config["EN"];
    const location = useLocation();
    const { modalState } = useModal();
    const { user } = useUser();
    const iconStyle = { fontSize: 16, marginLeft: "5px" };
    const { isLoggedIn } = useUser();
    const { toggleDarkMode, darkMode } = useDarkMode();

    const [anchorElNav, setAnchorElNav] = useState<null | HTMLElement>(null);
    const [anchorElUser, setAnchorElUser] = useState<null | HTMLElement>(null);

    const handleLogout = () => {
        handleLogout()
        navigate("/sign-in");
    };

    const pages = pagesData.pages.map((page) => ({ ...page }));

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
                    zIndex: modalState ? 11 : 1,
                    backgroundColor: darkMode ? "#111827" : "#fff",
                    boxShadow: "none",
                    borderBottom: scrolled ? "1px solid #ddd" : "none",
                }}
                className="mt-0"
            >
                <Container maxWidth="xl">
                    <Toolbar disableGutters>
                        <Link to="/">
                            <img height={40} width={40} src={logo} alt="" className="mr-2" />
                        </Link>
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
                                {pages.map((page, index) => (
                                    <MenuItem
                                        key={page.url}
                                        onClick={handleCloseNavMenu}
                                        className={`transition duration-300 ${index >= pages.length - 2 ? "ml-auto" : ""
                                            }`}
                                    >
                                        <NavLink
                                            to={page.url}
                                            className={`text-black  hover:text-blue-500 ${page.url === location.pathname ? "text-blue-500" : ""
                                                }`}
                                        >
                                            {page.name}
                                        </NavLink>
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
                            <div className="w-[150%]">
                                {pages.map((page, index) => (
                                    index !== pages.length && (
                                        <NavLink
                                            key={page.url}
                                            to={page.url}
                                            className={`text-black m-3 inline-block transition duration-300 border-b-2 border-transparent hover:border-blue-500 ${page.url === location.pathname ? "border-blue-300" : ""
                                                }`}
                                            onClick={handleCloseNavMenu}
                                        >
                                            <span
                                                className={`my-2 ${page.url === location.pathname
                                                    ? "text-blue-500"
                                                    : darkMode
                                                        ? "text-white"
                                                        : "text-black"
                                                    }`}
                                            >
                                                {page.name}
                                            </span>

                                        </NavLink>
                                    )
                                ))}

                            </div>
                        </Box>
                        {!darkMode ? (
                            <NightlightIcon
                                onClick={() => toggleDarkMode(darkMode)}
                                className="mr-5 text-[#6B7280] cursor-pointer hover:scale-105 duration-300"
                            />
                        ) : (
                            <Brightness4Icon
                                onClick={() => toggleDarkMode(darkMode)}
                                className="mr-5 text-[#fff] cursor-pointer hover:scale-105 duration-300"
                            />
                        )}
                        {!isLoggedIn() ? (
                            <button
                                type="button"
                                className="text-white bg-blue-700 hover:bg-blue-800  font-medium rounded-md text-sm px-3 py-1.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                            >
                                <a href="/sign-in">
                                    Sign In
                                    <EastSharpIcon style={iconStyle} />
                                </a>
                            </button>
                        ) : (
                            <Box sx={{ flexGrow: 0 }}>
                                <Tooltip title="Open settings">
                                    <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                                        <Avatar alt="Remy Sharp" src={userPicture} />
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
                                    <MenuItem
                                        sx={{ width: "200px", marginLeft: "10px" }}
                                    >
                                        <NavLink to="profile">Profile</NavLink>
                                    </MenuItem>
                                    {user.accountType === "Administrator" && (
                                        <>
                                            <MenuItem sx={{ width: "200px", marginLeft: "10px" }}>
                                                <NavLink to="blogs">Blogs</NavLink>
                                            </MenuItem>
                                            <MenuItem sx={{ width: "200px", marginLeft: "10px" }}>
                                                <NavLink to="reminders">Reminders</NavLink>
                                            </MenuItem>
                                        </>
                                    )}
                                    <MenuItem
                                        sx={{ width: "200px", marginLeft: "10px" }}
                                        onClick={() => handleLogout()}
                                    >
                                        <Typography textAlign="center">Log out</Typography>
                                    </MenuItem>
                                </Menu>
                            </Box>
                        )}
                    </Toolbar>
                </Container>
            </AppBar >
        </>
    );
};

export default NavBar;
