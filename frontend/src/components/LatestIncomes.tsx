import { useRef } from 'react';
import Slider from 'react-slick';
import EastSharpIcon from '@mui/icons-material/EastSharp';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';

const LatestIncomes = () => {
    const slider = useRef(null);

    const incomes = [
        { title: 'Income 1', amount: 50.00 },
        { title: 'Income 2', amount: 30.00 },
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
            name: 'Initial state',
            $: 0,
        },
        {
            name: 'Highest income',
            $: 5000,
        },
        {
            name: 'This income',
            $: 2000,
        },

    ];

    return (
        <>
            <Slider ref={slider} {...settings} className='w-1/2 rounded-lg m-10 mt-20'>
                {incomes.map((income, index) => (
                    <div key={index} className="bg-green-100 p-4 mx-4 flex justify-between items-center w-full">
                        <div>
                            <div className="text-xl font-bold mb-2">{income.title}</div>
                            <div className="text-2xl">${income.amount.toFixed(2)}</div>
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
                                <Line type="monotone" dataKey="$" stroke="#2563EB" />
                            </LineChart>
                        </div>
                    </div>
                ))}
            </Slider>
            <button className='mb-10' onClick={() => slider?.current?.slickNext()}>Next   <EastSharpIcon onClick={() => slider?.current?.slickNext()} /></button>
        </>
    );
};

export default LatestIncomes;
