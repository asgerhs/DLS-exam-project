FROM node:14-alpine AS development
ENV NODE_ENV development
# Add a work directory
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app
# Cache and Install dependencies
COPY package*.json /usr/src/app/
RUN npm install
# Copy app files
COPY . /usr/src/app
# Expose port
EXPOSE 3000
# Start the app
CMD [ "npm", "start" ]