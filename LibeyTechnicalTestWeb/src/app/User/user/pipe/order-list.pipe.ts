import { Pipe, PipeTransform } from '@angular/core';
import { LibeyUser } from 'src/app/entities/libeyuser';

@Pipe({
  name: 'orderList'
})
export class OrderListPipe implements PipeTransform {

  transform(value: Array<any>, property: string | null = null, sort: string = 'asc'): LibeyUser[] {
    try {
      if (!property) {
        return value; // Si no hay propiedad, retorna la lista sin cambios
      }

      const sortedList = value.sort((a, b) => {
        const aValue = this.getNestedProperty(a, property) ?? '';
        const bValue = this.getNestedProperty(b, property) ?? '';

        // Comparar valores
        if (aValue < bValue) return -1;
        if (aValue > bValue) return 1;
        return 0;
      });

      // Retornar en orden ascendente o descendente
      return sort === 'asc' ? sortedList : sortedList.reverse();
    } catch (error) {
      console.error('Error al ordenar la lista:', error);
      return value;
    }
  }

  private getNestedProperty(obj: any, path: string): any {
    return path.split('.').reduce((acc, key) => (acc ? acc[key] : undefined), obj);
  }
}
