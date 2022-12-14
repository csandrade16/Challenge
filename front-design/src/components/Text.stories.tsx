
import {Meta,StoryObj}  from '@storybook/react'
import { Text, TextProps } from "./Text";

export default{
    title: 'Components/Text',
    component: Text,
    args: {
        children: 'Calculo de PLR'
    }
} as Meta<TextProps>

export const Default: StoryObj = {}

export const Small: StoryObj = {
    args: {
        size: 'sm'
    }
}

export const Large: StoryObj = {
    args: {
        size: 'lg'
    }
}

export const CustomComponent: StoryObj <TextProps> = {
    args: {
        asChild: true,
    }
}