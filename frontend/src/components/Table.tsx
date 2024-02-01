import * as React from "react";
import { alpha } from "@mui/material/styles";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import TableSortLabel from "@mui/material/TableSortLabel";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import Checkbox from "@mui/material/Checkbox";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import DeleteIcon from "@mui/icons-material/Delete";
import FilterListIcon from "@mui/icons-material/FilterList";
import { visuallyHidden } from "@mui/utils";
import Skeleton from "@mui/material/Skeleton";
import { IncomeInterface } from "../interfaces/globalInterfaces";
import DeleteModal from "./Modals/DeleteModal";
import { Autocomplete, Popover, TextField } from "@mui/material";
import NewFormModal from "./Modals/NewFormModal";
import EditModal from "./Modals/EditModal";
import useObjectGroups from "../hooks/GlobalHooks/GetObjectsHook";
import useIncomes from "../hooks/Incomes/AllIncomesHook";
import { useModal } from "../contexts/GlobalContext";
import useDeleteAllObjects from "../hooks/GlobalHooks/DeleteAllObjectsHook";

interface Data {
    id: number;
    description: string;
    amount: number;
    incomeGroup: string;
}

type Order = "asc" | "desc";

function createData(
    id: number,
    description: string,
    amount: number,
    incomeGroup: string
): Data {
    return {
        id,
        description,
        amount,
        incomeGroup,
    };
}

function descendingComparator<T>(a: T, b: T, orderBy: keyof T) {
    if (b[orderBy] < a[orderBy]) {
        return -1;
    }
    if (b[orderBy] > a[orderBy]) {
        return 1;
    }
    return 0;
}

function getComparator<Key extends keyof Data>(
    order: Order,
    orderBy: Key
): (a: Data, b: Data) => number {
    return order === "desc"
        ? (a, b) => descendingComparator(a, b, orderBy)
        : (a, b) => -descendingComparator(a, b, orderBy);
}


function stableSort<T>(
    array: readonly T[],
    comparator: (a: T, b: T) => number
) {
    const stabilizedThis = array.map((el, index) => [el, index] as [T, number]);
    stabilizedThis.sort((a, b) => {
        const order = comparator(a[0], b[0]);
        if (order !== 0) {
            return order;
        }
        return a[1] - b[1];
    });
    return stabilizedThis.map((el) => el[0]);
}

interface HeadCell {
    disablePadding: boolean;
    id: keyof Data;
    label: string;
    numeric: boolean;
}

const headCells: readonly HeadCell[] = [
    {
        id: "id",
        numeric: false,
        disablePadding: true,
        label: "ID",
    },
    {
        id: "description",
        numeric: true,
        disablePadding: false,
        label: "Description",
    },
    {
        id: "amount",
        numeric: true,
        disablePadding: false,
        label: "Amount",
    },
    {
        id: "incomeGroup",
        numeric: true,
        disablePadding: false,
        label: "Income group",
    },
    {
        id: "incomeGroup",
        numeric: true,
        disablePadding: false,
        label: "Actions",
    },
];

interface EnhancedTableProps {
    numSelected: number;
    onRequestSort: (
        event: React.MouseEvent<unknown>,
        property: keyof Data
    ) => void;
    onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
    order: Order;
    orderBy: keyof Data;
    rowCount: number;
}

interface EnhancedTablePropsWithData {
    incomes: IncomeInterface[];
}

function EnhancedTableHead(props: EnhancedTableProps) {
    const {
        onSelectAllClick,
        order,
        orderBy,
        numSelected,
        rowCount,
        onRequestSort,
    } = props;
    const createSortHandler =
        (property: keyof Data) => (event: React.MouseEvent<unknown>) => {
            onRequestSort(event, property);
        };

    return (
        <TableHead>
            <TableRow>
                <TableCell padding="checkbox">
                    <Checkbox
                        color="primary"
                        indeterminate={
                            numSelected > 0 && numSelected < rowCount
                        }
                        checked={rowCount > 0 && numSelected === rowCount}
                        onChange={onSelectAllClick}
                        inputProps={{
                            "aria-label": "select all desserts",
                        }}
                    />
                </TableCell>
                {headCells.map((headCell) => (
                    <TableCell
                        key={headCell.id}
                        align={headCell.numeric ? "right" : "left"}
                        padding={headCell.disablePadding ? "none" : "normal"}
                        sortDirection={orderBy === headCell.id ? order : false}
                    >
                        <TableSortLabel
                            active={orderBy === headCell.id}
                            direction={orderBy === headCell.id ? order : "asc"}
                            onClick={createSortHandler(headCell.id)}
                        >
                            {headCell.label}
                            {orderBy === headCell.id ? (
                                <Box component="span" sx={visuallyHidden}>
                                    {order === "desc"
                                        ? "sorted descending"
                                        : "sorted ascending"}
                                </Box>
                            ) : null}
                        </TableSortLabel>
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}

interface EnhancedTableToolbarProps {
    numSelected: number;
}

interface ObjectGroup {
    id: number;
    name: string;
}

function EnhancedTableToolbar(props: EnhancedTableToolbarProps) {
    const { numSelected } = props;
    const [anchorEl, setAnchorEl] = React.useState<HTMLElement | null>(null);
    const [minAmount, setMinAmount] = React.useState<number | null>(null);
    const [maxAmount, setMaxAmount] = React.useState<number | null>(null);
    const [selectedIncomeGroup, setSelectedIncomeGroup] = React.useState<string>('');
    const { fetchObjectGroups, objectGroups } = useObjectGroups('income');
    const { setActionChanged, setAppliedFilters, getAppliedFilters } = useModal()
    const [searchTerm, setSearchTerm] = React.useState('');
    const { deleteAllObjects } = useDeleteAllObjects()

    const handleMinAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const newMinAmount = Number(e.target.value) || null;
        setMinAmount(newMinAmount);
        setActionChanged();
        setAppliedFilters({ ...getAppliedFilters(), minAmount: newMinAmount });
    };

    const handleMaxAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const newMaxAmount = Number(e.target.value) || null;
        setMaxAmount(newMaxAmount);
        setActionChanged();
        setAppliedFilters({ ...getAppliedFilters(), maxAmount: newMaxAmount });
    }

    const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const newSearchTerm = event.target.value;
        setSearchTerm(newSearchTerm);

        setTimeout(() => {
            setActionChanged();
            setAppliedFilters({ ...getAppliedFilters(), description: newSearchTerm });
        }, 0);
    };

    React.useEffect(() => {
        fetchObjectGroups()
    }, [])

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleIncomeGroupChange = (event: React.ChangeEvent<unknown>, newValue: ObjectGroup | null) => {
        setSelectedIncomeGroup((newValue?.id || '') as string);
        setActionChanged();
        setAppliedFilters({ ...getAppliedFilters(), incomeGroupId: (newValue?.id || '') as string });
    };

    const handleDeleteAllIncomes = async () => {
        await deleteAllObjects('income');
        setActionChanged();
    };

    const open = Boolean(anchorEl);
    const id = open ? 'simple-popover' : undefined;

    return (
        <Toolbar
            sx={{
                pl: { sm: 2 },
                pr: { xs: 1, sm: 1 },
                ...(numSelected > 0 && {
                    bgcolor: (theme) =>
                        alpha(
                            theme.palette.primary.main,
                            theme.palette.action.activatedOpacity
                        ),
                }),
            }}
        >
            {numSelected > 0 ? (
                <Typography
                    sx={{ flex: "1 1 100%" }}
                    color="inherit"
                    variant="subtitle1"
                    component="div"
                >
                    {numSelected} selected
                </Typography>
            ) : (
                <Typography
                    sx={{ flex: "1 1 100%" }}
                    variant="h6"
                    id="tableTitle"
                    component="div"
                >
                    Incomes
                </Typography>
            )}
            {numSelected > 0 ? (
                <Tooltip title="Delete">
                    <IconButton>
                        <DeleteIcon onClick={handleDeleteAllIncomes} />
                    </IconButton>
                </Tooltip>
            ) : (
                <div className="w-[340%] flex justify-end">
                    <NewFormModal />
                    <div>
                        <label htmlFor="default-search" className="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">
                            Search
                        </label>
                        <div className="relative">
                            <div className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
                                <svg
                                    className="w-4 h-4 text-gray-500 dark:text-gray-400"
                                    aria-hidden="true"
                                    xmlns="http://www.w3.org/2000/svg"
                                    fill="none"
                                    viewBox="0 0 20 20"
                                >
                                    <path
                                        stroke="currentColor"
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                        strokeWidth="2"
                                        d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
                                    />
                                </svg>
                            </div>
                            <input
                                type="search"
                                id="default-search"
                                className="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg outline-none "
                                placeholder="Search..."
                                required
                                value={searchTerm}
                                onChange={handleSearchChange}
                            />
                        </div>
                    </div>

                    <div>
                        <Tooltip title="Filter list">
                            <IconButton onClick={handleClick}>
                                <FilterListIcon />
                            </IconButton>
                        </Tooltip>

                        <Popover
                            id={id}
                            open={open}
                            anchorEl={anchorEl}
                            onClose={handleClose}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'right',
                            }}
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                        >
                            <div className="p-3">
                                <TextField
                                    label="Min Amount"
                                    type="number"
                                    variant="standard"
                                    value={minAmount || ''}
                                    onChange={handleMinAmountChange}
                                />
                            </div>
                            <div className="p-3">
                                <TextField
                                    label="Max Amount"
                                    type="number"
                                    variant="standard"
                                    value={maxAmount || ''}
                                    onChange={handleMaxAmountChange}
                                />
                            </div>
                            <Autocomplete
                                style={{ padding: "10px" }}
                                // onOpen={handleAutocompleteOpen}
                                options={objectGroups}
                                getOptionLabel={(option) => option.name}
                                sx={{ width: '100%', marginTop: '20px' }}
                                renderInput={(params) => (
                                    <TextField {...params} label="Income group" required />
                                )}
                                value={objectGroups.find((group) => group.id === Number(selectedIncomeGroup))}
                                onChange={handleIncomeGroupChange}
                            />
                        </Popover>
                    </div>


                </div>
            )}
        </Toolbar>
    );
}

function LoadingTableRow() {
    return (
        <TableRow>
            <TableCell>
                <Skeleton />
            </TableCell>
            <TableCell>
                <Skeleton />
            </TableCell>
            <TableCell>
                <Skeleton />
            </TableCell>
            <TableCell>
                <Skeleton />
            </TableCell>
            <TableCell>
                <Skeleton />
            </TableCell>
            <TableCell>
                <Skeleton />
            </TableCell>
        </TableRow>
    );
}

function EnhancedTable({ incomes }: EnhancedTablePropsWithData) {
    const [order, setOrder] = React.useState<Order>('desc');
    const [orderBy, setOrderBy] = React.useState<keyof Data>('id');
    const [selected, setSelected] = React.useState<readonly number[]>([]);
    const [page, setPage] = React.useState(0);
    const [dense] = React.useState(false);
    const [loading, setLoading] = React.useState(true);
    const { setActionChanged } = useModal()
    const { rowsPerPage, setRowsPerPage } = useIncomes();
    const { getAppliedFilters, setAppliedFilters, totalRecords, pageNumber } = useModal()

    React.useEffect(() => {
        const newRows = incomes.map((income) =>
            createData(
                income.id,
                income.description,
                income.amount,
                income.incomeGroup?.name ?? ''
            )
        );
        setRows(newRows);

        const timeout = setTimeout(() => {
            setLoading(false);
        }, 1000);

        return () => clearTimeout(timeout);
    }, [incomes]);

    const [rows, setRows] = React.useState<Data[]>([]);

    React.useEffect(() => {
        const timeout = setTimeout(() => {
            setLoading(false);
        }, 2000);

        return () => clearTimeout(timeout);
    }, []);

    const handleRequestSort = (
        _: React.MouseEvent<unknown>,
        property: keyof Data
    ) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    const handleSelectAllClick = (
        event: React.ChangeEvent<HTMLInputElement>
    ) => {
        if (event.target.checked) {
            const newSelected = rows.map((n) => n.id);
            setSelected(newSelected);
            return;
        }
        setSelected([]);
    };

    const handleChangePage = (_event: unknown, newPage: number) => {
        setPage(newPage);
        setAppliedFilters({
            ...getAppliedFilters(),
            pageNumber: newPage + 1,
        });
        setActionChanged();
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement>
    ) => {
        setRowsPerPage(parseInt(event.target.value));
        setAppliedFilters({ ...getAppliedFilters(), pageSize: parseInt(event.target.value) });
        setActionChanged()
        setPage(0);
    };

    const isSelected = (id: number) => selected.indexOf(id) !== -1;

    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    const visibleRows = React.useMemo(() => {
        return stableSort(rows, getComparator(order, orderBy)).slice(
            page * rowsPerPage,
            page * rowsPerPage + rowsPerPage
        );
    }, [rows, order, orderBy, page, rowsPerPage]);

    return (
        <Box sx={{ width: '100%' }}>
            <Paper sx={{ width: '100%', mb: 2 }}>
                <EnhancedTableToolbar numSelected={selected.length} />
                <TableContainer>
                    <Table
                        sx={{ minWidth: 750 }}
                        aria-labelledby="tableTitle"
                        size={dense ? 'small' : 'medium'}
                    >
                        <EnhancedTableHead
                            numSelected={selected.length}
                            order={order}
                            orderBy={orderBy}
                            onSelectAllClick={handleSelectAllClick}
                            onRequestSort={handleRequestSort}
                            rowCount={rows.length}
                        />
                        <TableBody>
                            {loading
                                ? Array.from({
                                    length: rowsPerPage,
                                }).map((_, index) => (
                                    <LoadingTableRow key={index} />
                                ))
                                : visibleRows.map((row) => (
                                    <TableRow
                                        hover
                                        role="checkbox"
                                        aria-checked={isSelected(
                                            row.id
                                        )}
                                        tabIndex={-1}
                                        key={row.id}
                                        selected={isSelected(row.id)}
                                        sx={{ cursor: 'pointer' }}
                                    >
                                        <TableCell padding="checkbox">
                                            {/* <Checkbox
                                                     color="primary"
                                                     checked={isSelected(
                                                         row.id
                                                     )}
                                                     inputProps={{
                                                         'aria-labelledby':
                                                             `enhanced-table-checkbox-${index}`,
                                                     }}
                                                 /> */}
                                        </TableCell>
                                        <TableCell
                                            component="th"
                                            scope="row"
                                            padding="none"
                                        >
                                            {row.id}
                                        </TableCell>
                                        <TableCell align="right">
                                            {row.description}
                                        </TableCell>
                                        <TableCell align="right">
                                            {row.amount}
                                        </TableCell>
                                        <TableCell align="right">
                                            {row.incomeGroup}
                                        </TableCell>
                                        <TableCell align="right">
                                            <EditModal id={row.id} objectType={""} />
                                            <DeleteModal
                                                id={row.id}
                                                objectType={
                                                    'income'
                                                }
                                            />
                                        </TableCell>
                                    </TableRow>
                                ))}
                            <TableRow
                                style={{
                                    height:
                                        (dense ? 33 : 53) *
                                        emptyRows,
                                }}
                            >
                                <TableCell colSpan={6} />
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    rowsPerPageOptions={[5, 10, 25]}
                    component="div"
                    count={totalRecords}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onPageChange={handleChangePage}
                    onRowsPerPageChange={
                        handleChangeRowsPerPage
                    }
                />
            </Paper>
        </Box >
    );
}

export default EnhancedTable;
