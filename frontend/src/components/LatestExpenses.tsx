import { useRef } from 'react';
import Slider from 'react-slick';
import EastSharpIcon from '@mui/icons-material/EastSharp';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';

const LatestExpenses = () => {
    const slider = useRef(null);

    const expenses = [
        { title: 'Expense 1', amount: 50.00 },
        { title: 'Expense 2', amount: 30.00 },
    ];

    const settings = {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1
    };

    const data = [
        {
            name: 'Highest expense',
            $: 5000,
        },
        {
            name: 'This expense',
            $: 3000,
        },
        {
            name: 'The worst state',
            $: 0,
        },

    ];

    return (
        <>
            <Slider ref={slider} {...settings} className='w-1/2 rounded-lg m-10 mt-20'>
                {expenses.map((expense, index) => (
                    <div key={index} className="bg-red-100 p-4 mx-4 flex justify-between items-center w-full">
                        <div>
                            <div className="text-xl font-bold mb-2">{expense.title}</div>
                            <div className="text-2xl">${expense.amount.toFixed(2)}</div>
                        </div>
                        <div className='mr-10 float-right'>
                            <LineChart
                                width={500}
                                height={200}
                                data={data}
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
                                <Line type="monotone" dataKey="$" stroke="red" />
                            </LineChart>
                        </div>
                    </div>
                ))}
            </Slider>
            <button className='mb-10' onClick={() => slider?.current?.slickNext()}>Next   <EastSharpIcon onClick={() => slider?.current?.slickNext()} /></button>
        </>
    );
};

export default LatestExpenses;
