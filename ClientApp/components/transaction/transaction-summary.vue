<template>
    <div>      
        <g-chart class="transaction-summary-chart"
          type="PieChart"
          :data="chartData"
          :options="chartOptions"
        />
        <div class="balance-container">          
            <span>Balance: </span><b>{{displayedBalance}}</b>
        </div>
              
        <div class="create-new-transaction">
            <button class="btn btn-danger round-button" @click="createTransaction(0)">Add Expense</button>
            <button class="btn btn-success round-button" @click="createTransaction(1)">Add Income</button>
        </div>
    </div>
</template>

<script>
import { GChart } from "vue-google-charts";

export default {
  components: {
    "g-chart": GChart
  },
  props: [
    'transactions'
  ],
  data: function() {
    return {
      chartData: [
        ["Task", "Hours per Day"],
        ["Work", 11],
        ["Eat", 2],
        ["Commute", 2],
        ["Watch TV", 2],
        ["Sleep", 7]
      ],
      chartOptions: {
        title: 'Transaction Summary',
        pieHole: 0.4,
        legend: 'none'
      }
    };
  },
  computed: {
    displayedBalance() {
      let balance = 0;
      balance = this.transactions
        .map(x => {
          let factor = x.type == 0 ? -1 : 1;
          return x.changeValue * factor;
        })
        .reduce((sum, value) => {
          return (sum += value);
        }, 0);
      return balance;
    }
  },
  methods: {
    createTransaction(transactionType) {
        this.$emit('create-transaction', transactionType);
    }
  }
};
</script>

<style scoped>
  .transaction-summary-chart {
    display: flex;
    justify-content: space-between;
    width: 50%;
    margin: auto;
    height: 400px;
  }

  .balance-container{
    text-align: center;
    font-size: 30px;
  }
    
  .create-new-transaction {
    display: flex;
    justify-content: space-between;
    margin: auto;
    width: 80%;
    max-width: 400px;
  }
</style>
