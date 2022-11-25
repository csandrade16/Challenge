import { clsx } from 'clsx';
import { Slot } from '@radix-ui/react-slot';

export interface TextProps{
    size?: 'sm' |'md'| 'lg';
    children: string;
    asChild?: boolean;
}


export function Text({size = 'md', children, asChild }: TextProps){
    const Comp = asChild ? Slot : 'span';

    return(
        <span 
        className={clsx(
            { 
                'text-xs': size == 'sm',
                'text-sm': size == 'md',
                'text-md': size == 'lg',
            }
        )}
        >
        {children}
        </span>
    )
}