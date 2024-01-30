import { useEffect, useRef } from 'react';
import Slider from 'react-slick';
import EastSharpIcon from '@mui/icons-material/EastSharp';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import AttachMoneyIcon from '@mui/icons-material/AttachMoney';
import useLatestExpenses from '../hooks/Expenses/LatestExpensesHook';
import { formatDate } from '../utils/utils';

const LatestExpenses = () => {
    const slider = useRef<Slider | null>(null);
    const { expenses, loadLatestExpenses, highestExpense } = useLatestExpenses();

    useEffect(() => {
        const fetchData = async () => {
            await loadLatestExpenses();
        };

        setTimeout(() => {
            fetchData();
        }, 1000);
    }, []);

    const settings = {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1
    };

    const expenseData = expenses.map((expense) => [
        {
            name: 'The worst state',
            $: 0,
        },
        {
            name: 'Highest expense',
            $: highestExpense,
        },
        {
            name: 'This expense',
            $: expense.amount,
        },
    ]);

    return (
        <>
            <Slider ref={slider} {...settings} className='w-[90%] lg:w-1/2 xs:w-1/2 rounded-lg m-10 mt-20'>
                {expenses.map((expense, index) => (
                    <div key={index} className="bg-red-100 p-4 mx-4 flex justify-between items-center w-full">
                        <div>
                            <div className="text-xl font-bold mb-2">{expense.description}</div>
                            <div className="text-2xl">${expense.amount.toFixed(2)}</div>
                            <p>{formatDate(expense.createdAt)}</p>
                        </div>
                        <div className="hidden-sm float-right">
                            <LineChart
                                key={index}
                                width={500}
                                height={200}
                                data={expenseData[index]}
                                margin={{
                                    top: 5,
                                    right: 30,
                                    left: 20,
                                    bottom: 5,
                                }}
                            >
                                <CartesianGrid strokeDasharray="3 3" />
                                <XAxis dataKey="name" />
                                <YAxis />
                                <Tooltip />
                                <Legend />
                                <Line type="monotone" dataKey="$" stroke="#2563EB" />
                            </LineChart>
                        </div>
                        <div className="sm:hidden float-right">
                            <AttachMoneyIcon className='text-red-500 mr-2' />
                        </div>
                    </div>
                ))}
            </Slider>
            <button className='mb-10' onClick={() => slider?.current?.slickNext()}>Next <EastSharpIcon onClick={() => slider?.current?.slickNext()} /></button>
        </>
    );
};

export default LatestExpenses;
