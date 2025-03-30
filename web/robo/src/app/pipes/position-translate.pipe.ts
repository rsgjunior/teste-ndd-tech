import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'positionTranslate',
    standalone: true
})
export class PositionTranslatePipe implements PipeTransform {
    private translations: { [key: string]: string } = {
        'Rest': 'Repouso',
        'SlightlyContracted': 'Levemente Contraído',
        'Contracted': 'Contraído',
        'StronglyContracted': 'Fortemente Contraído',

        'Rotation45': 'Rotação 45°',
        'Rotation90': 'Rotação 90°',
        'Rotation135': 'Rotação 135°',
        'Rotation180': 'Rotação 180°',

        'Rotation90Negative': 'Rotação -90°',
        'Rotation45Negative': 'Rotação -45°',
        'RotationZero': 'Rotação 0°',

        'Up': 'Para Cima',
        'Down': 'Para Baixo',
        'Zero': 'Centralizado'
    };

    transform(value: string): string {
        return this.translations[value] || value;
    }
}