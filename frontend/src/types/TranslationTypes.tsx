export type Page = {
    name: string;
    url: string;
};

export type LanguageConfig = {
    acceptButton: string;
    privacyPolicy: string;
    policyText: string;
    pages: Page[];
};

export type Config = {
    [key: string]: LanguageConfig;
};