import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { MailchimpSubscribeHookResult } from '../../interfaces/globalInterfaces';

const useMailchimpSubscribe = (): MailchimpSubscribeHookResult => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const subscribeToMailchimp = async (email: string): Promise<void> => {
        setIsLoading(true);

        try {
            const response = await axiosConfig.post('/api/mailchimp/subscribe', { Email: email });
            if (response.data && response.data.success) {
                setSuccess(true);
                setError(null);
            } else {
                setSuccess(false);
                setError('Failed to subscribe to Mailchimp');
            }
        } catch (err) {
            setSuccess(false);
            setError('Error subscribing to Mailchimp');
        } finally {
            setIsLoading(false);
        }
    };

    return { isLoading, success, error, subscribeToMailchimp };
};

export default useMailchimpSubscribe;
