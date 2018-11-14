<template>
    <div>
        <h3>Transaction Summary</h3>        
        <g-chart class="transaction-summary-chart"
          type="PieChart"
          :data="chartData"
          :options="chartOptions"
        />
        <div class="create-spending-item">            
            <button class="btn btn-danger" @click="createTransaction(0)">Expense</button>
            <button class="btn btn-success" @click="createTransaction(1)">Income</button>
        </div>
    </div>
</template>

<script>
import { GChart } from "vue-google-charts";

export default {
  components: {
    "g-chart": GChart
  },
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
        title: 'My Daily Activities',
        pieHole: 0.5
      }
    };
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
    width: 900px;
    height: 400px;
  }
</style>
