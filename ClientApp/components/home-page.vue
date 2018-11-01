<template>
    <div>   
        <h4>Balance: {{balance}}</h4>  
    </div>
</template>

<script>
export default {
  data() {
    return {
      spendingItems: [
        { id: "1", changeValue: 50, type: 1 },
        { id: "2", changeValue: 20, type: 1 },
        { id: "3", changeValue: 70, type: 1 },
        { id: "4", changeValue: 90, type: 0 }
      ]
    };
  },
  computed: {
    balance() {
      return this.spendingItems
        .map(x => {
          let factor = x.type === 0 ? -1 : 1;
          return x.changeValue * factor;
        })
        .reduce((total, num) => total + Math.round(num), 0);
    }
  }
};
</script>

<style>
</style>
